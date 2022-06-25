using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public async Task<int> GetBlogTotalPagesAsync()
    {
        List<int> totalPagesCount = new();
        Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "blogs");
        QueryDefinition query = new("SELECT DISTINCT VALUE n.itemCount FROM (SELECT COUNT(1) AS itemCount FROM c WHERE c.partitionKey = 'blog-entry' AND c.blogIsPublished = true ) n");

        FeedIterator<int> containerQueryIterator = container.GetItemQueryIterator<int>(query);
        while (containerQueryIterator.HasMoreResults)
        {
            foreach (int item in await containerQueryIterator.ReadNextAsync())
            {
                totalPagesCount.Add(item);
            }
        }

        return (int)Math.Round((decimal)totalPagesCount[0] / 5, 0, MidpointRounding.ToPositiveInfinity);
    }
}