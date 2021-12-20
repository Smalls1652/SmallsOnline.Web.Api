using SmallsOnline.Web.Api.Lib.Models.Albums;
using SmallsOnline.Web.Api.Lib.Models.Tracks;

namespace SmallsOnline.Web.Api.Services;

public interface ICosmosDbService
{
    List<AlbumData> GetFavoriteAlbumsOfYear(string listYear);
    List<TrackData> GetFavoriteTracksOfYear(string listYear);
}