namespace SmallsOnline.Web.Api.Lib.Models.Albums;

public interface IAlbumData
{
    string? Title { get; set; }
    string? Artist { get; set; }
    AlbumStandoutTrack[]? StandoutTracks { get; set; }
    string? AlbumArtUrl { get; set; }
    string? AlbumUrl { get; set; }
    bool IsBest { get; set; }
    string? Comments { get; set; }
    string? ListYear { get; set; }
}