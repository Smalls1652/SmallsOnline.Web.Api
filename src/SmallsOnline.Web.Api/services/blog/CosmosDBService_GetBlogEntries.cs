using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public async Task<List<BlogListEntry>> GetBlogEntriesAsync(int pageNumber = 1)
    {
        List<BlogListEntry> blogEntries = new();

        int offsetNum = pageNumber switch
        {
            0 => throw new Exception("Invalid page number."),
            1 => 0,
            _ => (pageNumber - 1) * 5
        };

        Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "blogs");
        QueryDefinition query = new($"SELECT c.id, c.partitionKey, c.blogTitle, c.blogPostedDate, c.blogTags FROM c WHERE c.partitionKey = \"blog-entry\" AND c.blogIsPublished = true ORDER BY c.blogPostedDate DESC OFFSET {offsetNum} LIMIT 5");

        FeedIterator<BlogListEntry> containerQueryIterator = container.GetItemQueryIterator<BlogListEntry>(query);
        while (containerQueryIterator.HasMoreResults)
        {
            foreach (BlogListEntry item in await containerQueryIterator.ReadNextAsync())
            {
                blogEntries.Add(item);
            }
        }

        containerQueryIterator.Dispose();

        return blogEntries;
    }
}