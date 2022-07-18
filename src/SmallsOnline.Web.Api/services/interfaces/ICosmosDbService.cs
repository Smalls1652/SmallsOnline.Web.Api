namespace SmallsOnline.Web.Api.Services;

public interface ICosmosDbService
{
    Task<List<BlogEntry>> GetBlogEntriesAsync(int pageNumber = 1);
    Task<BlogEntry> GetBlogEntryAsync(string id);
    Task<int> GetBlogTotalPagesAsync();

    Task<List<AlbumData>> GetFavoriteAlbumsOfYearAsync(string listYear);
    Task<List<TrackData>> GetFavoriteTracksOfYearAsync(string listYear);
}