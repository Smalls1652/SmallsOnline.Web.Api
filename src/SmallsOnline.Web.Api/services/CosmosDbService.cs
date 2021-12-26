using System.Text.Json.Serialization;

using Microsoft.Azure.Cosmos;

using SmallsOnline.Web.Api.Helpers;
using SmallsOnline.Web.Lib.Models.Json;
using SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;
using SmallsOnline.Web.Lib.Models.FavoritesOf.Tracks;

namespace SmallsOnline.Web.Api.Services;

public class CosmosDbService : ICosmosDbService
{
    public CosmosDbService()
    {
        InitService();
    }

    private CosmosClient? cosmosDbClient;
    private readonly CosmosDbSerializer jsonSerializer = new(
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = {
                new JsonDateTimeOffsetConverter()
            }
        }
    );

    public List<AlbumData> GetFavoriteAlbumsOfYear(string listYear)
    {
        Task<List<AlbumData>> getFromDbTask = Task.Run(
            async () =>
            {
                List<AlbumData> albumItems = new();

                Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "top-music-albums");
                QueryDefinition query = new($"SELECT * FROM c WHERE c.listYear = \"{listYear}\"");

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
        );

        getFromDbTask.Wait();

        return getFromDbTask.Result;
    }

    public List<TrackData> GetFavoriteTracksOfYear(string listYear)
    {
        Task<List<TrackData>> getFromDbTask = Task.Run(
            async () =>
            {
                List<TrackData> trackItems = new();

                Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "top-music-tracks");
                QueryDefinition query = new($"SELECT * FROM c WHERE c.listYear = \"{listYear}\"");

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

    private void InitService()
    {
        cosmosDbClient = new(
            connectionString: AppSettings.GetSetting("CosmosDbConnectionString"),
            clientOptions: new()
            {
                Serializer = jsonSerializer
            }
        );
    }
}