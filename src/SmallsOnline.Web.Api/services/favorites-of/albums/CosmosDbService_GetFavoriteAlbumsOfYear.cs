using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public List<AlbumData> GetFavoriteAlbumsOfYear(string listYear)
    {
        Task<List<AlbumData>> getFromDbTask = Task.Run(async () => await GetFavoriteAlbumsOfYearAsync(listYear));

        getFromDbTask.Wait();

        return getFromDbTask.Result;
    }

    public async Task<List<AlbumData>> GetFavoriteAlbumsOfYearAsync(string listYear)
    {
        List<AlbumData> albumItems = new();

        Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "favorites-of");
        QueryDefinition query = new($"SELECT * FROM c WHERE c.partitionKey = \"favorites-of-albums\" AND c.listYear = \"{listYear}\"");

        FeedIterator<AlbumData> containerQueryIterator = container.GetItemQueryIterator<AlbumData>(query);
        while (containerQueryIterator.HasMoreResults)
        {
            foreach (AlbumData item in await containerQueryIterator.ReadNextAsync())
            {
                albumItems.Add(item);
            }
        }

        containerQueryIterator.Dispose();

        return albumItems;
    }
}