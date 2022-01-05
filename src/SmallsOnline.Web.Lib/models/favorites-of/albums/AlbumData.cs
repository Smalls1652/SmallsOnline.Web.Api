using System.Collections.Generic;

namespace SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;

public class AlbumData : IAlbumData
{
    public AlbumData() { }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("partitionKey")]
    public string? PartitionKey { get; set; }

    [JsonPropertyName("albumTitle")]
    public string? Title { get; set; }

    [JsonPropertyName("albumArtist")]
    public string? Artist { get; set; }

    [JsonPropertyName("albumStandoutTracks")]
    public List<AlbumStandoutTrack> StandoutTracks { get; set; }

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