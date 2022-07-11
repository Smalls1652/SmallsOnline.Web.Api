namespace SmallsOnline.Web.Api.Services;

public interface ICosmosDbService
{
    Task<List<BlogEntry>> GetBlogEntriesAsync(int pageNumber = 1);
    Task<BlogEntry> GetBlogEntryAsync(string id);
    Task<int> GetBlogTotalPagesAsync();

    List<AlbumData> GetFavoriteAlbumsOfYear(string listYear);
    Task<List<AlbumData>> GetFavoriteAlbumsOfYearAsync(string listYear);
    
    List<TrackData> GetFavoriteTracksOfYear(string listYear);
    Task<List<TrackData>> GetFavoriteTracksOfYearAsync(string listYear);
}