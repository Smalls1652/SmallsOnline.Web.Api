namespace SmallsOnline.Web.Api.Lib.Models.Tracks;

public interface ITrackData
{
    string? Id { get; set; }
    string? Title { get; set; }
    string? Artist { get; set; }
    string? TrackArtUrl { get; set; }
    string? TrackUrl { get; set; }
    string? Comments { get; set; }
    string? ListYear { get; set; }
}