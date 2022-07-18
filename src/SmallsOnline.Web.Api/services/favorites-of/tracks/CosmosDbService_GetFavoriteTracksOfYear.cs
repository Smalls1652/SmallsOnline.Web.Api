using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public List<TrackData> GetFavoriteTracksOfYear(string listYear)
    {
        Task<List<TrackData>> getFromDbTask = Task.Run(async () => await GetFavoriteTracksOfYearAsync(listYear));

        getFromDbTask.Wait();

        return getFromDbTask.Result;
    }

    /// <summary>
    /// Get the favorite tracks for a specific year.
    /// </summary>
    /// <param name="listYear">The list year to get the data for.</param>
    /// <returns>A collection of favorite tracks for a year</returns>
    public async Task<List<TrackData>> GetFavoriteTracksOfYearAsync(string listYear)
    {
        // Create a list to hold the track items.
        List<TrackData> trackItems = new();

        // Get the container where the favorite music entries are stored.
        Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "favorites-of");

        // Define the query for getting the favorite tracks for a year.
        QueryDefinition query = new($"SELECT * FROM c WHERE c.partitionKey = \"favorites-of-tracks\" AND c.listYear = \"{listYear}\"");

        // Execute the query.
        FeedIterator<TrackData> containerQueryIterator = container.GetItemQueryIterator<TrackData>(query);
        while (containerQueryIterator.HasMoreResults)
        {
            foreach (TrackData item in await containerQueryIterator.ReadNextAsync())
            {
                // Add the track to the list.
                trackItems.Add(item);
            }
        }

        // Dispose of the query iterator.
        containerQueryIterator.Dispose();

        return trackItems;
    }
}