using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public List<TrackData> GetFavoriteTracksOfYear(string listYear)
    {
        Task<List<TrackData>> getFromDbTask = Task.Run(
            async () =>
            {
                List<TrackData> trackItems = new();

                Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "favorites-of");
                QueryDefinition query = new($"SELECT * FROM c WHERE c.partitionKey = \"favorites-of-tracks\" AND c.listYear = \"{listYear}\"");

                FeedIterator<TrackData> containerQueryIterator = container.GetItemQueryIterator<TrackData>(query);
                while (containerQueryIterator.HasMoreResults)
                {
                    foreach (TrackData item in await containerQueryIterator.ReadNextAsync())
                    {
                        trackItems.Add(item);
                    }
                }

                containerQueryIterator.Dispose();

                return trackItems;
            }
        );

        getFromDbTask.Wait();

        return getFromDbTask.Result;
    }
}