using SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;
using SmallsOnline.Web.Lib.Models.FavoritesOf.Tracks;

namespace SmallsOnline.Web.Api.Services;

public interface ICosmosDbService
{
    List<AlbumData> GetFavoriteAlbumsOfYear(string listYear);
    List<TrackData> GetFavoriteTracksOfYear(string listYear);
}