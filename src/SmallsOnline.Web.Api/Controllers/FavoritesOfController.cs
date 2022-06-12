using Microsoft.AspNetCore.Mvc;

namespace SmallsOnline.Web.Api.Controllers;

[ApiController]
[Route("api/favoritesOf")]
public class FavoriteOfController : ControllerBase
{
    private readonly ILogger<FavoriteOfController> _logger;
    private readonly ICosmosDbService _cosmosDbService;

    public FavoriteOfController(ILogger<FavoriteOfController> logger, ICosmosDbService cosmosDbService)
    {
        _logger = logger;
        _cosmosDbService = cosmosDbService;
    }

    [HttpGet("albums/{year}" ,Name = "GetFavoriteAlbums")]
    public async Task<IEnumerable<AlbumData>> GetFavoriteAlbums(string year)
    {
        _logger.LogInformation("Getting favorite albums for {year}.", year);
        List<AlbumData> retrievedAlbums = await _cosmosDbService.GetFavoriteAlbumsOfYearAsync(
            listYear: year
        );

        retrievedAlbums.Sort(
            (AlbumData album1, AlbumData album2) => album2.IsBest.CompareTo(album1.IsBest)
        );

        if (retrievedAlbums.Count > 1 && retrievedAlbums.Count != 0)
        {
            retrievedAlbums.Sort(
                index: 1,
                count: retrievedAlbums.Count - 1,
                comparer: new AlbumReleaseDateComparer()
            );
        }

        JsonSerializerOptions serializerOptions = new()
        {
            Converters = {
                new JsonDateTimeOffsetConverter()
            }
        };

        return retrievedAlbums.ToArray();
    }

    [HttpGet("tracks/{year}", Name = "GetFavoriteTracks")]
    public async Task<IEnumerable<TrackData>> GetFavoriteTracks(string year)
    {
        _logger.LogInformation("Getting favorite tracks for {year}.", year);
        List<TrackData> retrievedTracks = await _cosmosDbService.GetFavoriteTracksOfYearAsync(
            listYear: year
        );

        if (retrievedTracks.Count > 1)
        {
            retrievedTracks.Sort(
                comparer: new TrackReleaseDateComparer()
            );
        }

        return retrievedTracks.ToArray();
    }
}