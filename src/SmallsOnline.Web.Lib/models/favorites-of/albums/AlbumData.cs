namespace SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;

public class AlbumData : IAlbumData
{
    public AlbumData() { }

    [JsonPropertyName("albumTitle")]
    public string? Title { get; set; }

    [JsonPropertyName("albumArtist")]
    public string? Artist { get; set; }

    [JsonPropertyName("albumStandoutTracks")]
    public AlbumStandoutTrack[]? StandoutTracks { get; set; }

    [JsonPropertyName("albumArtUrl")]
    public string? AlbumArtUrl { get; set; }

    [JsonPropertyName("albumUrl")]
    public string? AlbumUrl { get; set; }

    [JsonPropertyName("albumIsBest")]
    public bool IsBest { get; set; }

    [JsonPropertyName("albumComments")]
    public string? Comments { get; set; }

    [JsonPropertyName("albumReleaseDate")]
    public DateTimeOffset? ReleaseDate { get; set; }

    [JsonPropertyName("listYear")]
    public string? ListYear { get; set; }
}