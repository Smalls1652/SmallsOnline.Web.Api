using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public async Task<BlogEntry> GetBlogEntryAsync(string id)
    {
        Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "blogs");

        BlogEntry retrievedItem = await container.ReadItemAsync<BlogEntry>(
            id: id,
            partitionKey: new("blog-entry")
        );

        return retrievedItem;
    }
}