using System.Text;
using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public async Task<List<BlogEntry>> GetBlogEntriesAsync(int pageNumber = 1)
    {
        List<BlogEntry> blogEntries = new();

        int offsetNum = pageNumber switch
        {
            0 => throw new Exception("Invalid page number."),
            1 => 0,
            _ => (pageNumber - 1) * 5
        };

        Container container = cosmosDbClient.GetContainer(AppSettings.GetSetting("CosmosDbContainerName"), "blogs");
        QueryDefinition query = new($"SELECT c.id, c.partitionKey, c.blogTitle, c.blogPostedDate, c.blogContent, c.blogTags, c.blogIsPublished FROM c WHERE c.partitionKey = \"blog-entry\" AND c.blogIsPublished = true ORDER BY c.blogPostedDate DESC OFFSET {offsetNum} LIMIT 5");

        FeedIterator<BlogEntry> containerQueryIterator = container.GetItemQueryIterator<BlogEntry>(query);
        while (containerQueryIterator.HasMoreResults)
        {
            foreach (BlogEntry item in await containerQueryIterator.ReadNextAsync())
            {
                if (item.Content is not null)
                {
                    StringBuilder markdownShort = new();
                    using (StringReader stringReader = new(item.Content))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            string? line = await stringReader.ReadLineAsync();
                            if (i < 4 || (i == 4 && line is not null))
                            {
                                markdownShort.AppendLine(line);
                            }
                        }
                    }

                    item.Content = markdownShort.ToString();
                }

                blogEntries.Add(item);
            }
        }

        containerQueryIterator.Dispose();

        return blogEntries;
    }
}