using Microsoft.AspNetCore.Mvc;

namespace SmallsOnline.Web.Api.Controllers;

[ApiController]
[Route("api/blog")]
public class BlogController : ControllerBase
{
    private readonly ILogger<BlogController> _logger;
    private readonly ICosmosDbService _cosmosDbService;

    public BlogController(ILogger<BlogController> logger, ICosmosDbService cosmosDbService)
    {
        _logger = logger;
        _cosmosDbService = cosmosDbService;
    }
    
    [HttpGet("entries/{pageNumber?}", Name = "GetBlogEntries")]
    public async Task<BlogEntries> GetBlogEntries(int pageNumber = 1)
    {
        _logger.LogInformation("Getting blog entries.");

        int totalPages = await _cosmosDbService.GetBlogTotalPagesAsync();
        List<BlogListEntry> retrievedBlogEntries = await _cosmosDbService.GetBlogEntriesAsync(pageNumber);

        return new()
        {
            PageNumber = pageNumber,
            TotalPages = totalPages,
            Entries = retrievedBlogEntries
        };
    }

    [HttpGet("entry/{id}", Name = "GetBlogEntry")]
    public async Task<BlogEntry> GetBlogEntry(string id)
    {
        _logger.LogInformation("Getting blog entry for id '{id}'.", id);

        BlogEntry retrievedBlogEntry = await _cosmosDbService.GetBlogEntryAsync(id);

        return retrievedBlogEntry;
    }
}