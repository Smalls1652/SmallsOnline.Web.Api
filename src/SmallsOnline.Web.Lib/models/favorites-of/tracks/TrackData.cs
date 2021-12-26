namespace SmallsOnline.Web.Lib.Models.FavoritesOf.Tracks;

public class TrackData : ITrackData
{
    public TrackData() {}

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("partitionKey")]
    public string? PartitionKey { get; set; }

    [JsonPropertyName("trackTitle")]
    public string? Title { get; set; }

    [JsonPropertyName("trackArtist")]
    public string? Artist { get; set; }

    [JsonPropertyName("trackArtUrl")]
    public string? TrackArtUrl { get; set; }

    [JsonPropertyName("trackUrl")]
    public string? TrackUrl { get; set; }

    [JsonPropertyName("trackComments")]
    public string? Comments { get; set; }

    [JsonPropertyName("listYear")]
    public string? ListYear { get; set; }
}