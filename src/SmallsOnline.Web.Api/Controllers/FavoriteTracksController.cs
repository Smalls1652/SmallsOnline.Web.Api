using Microsoft.AspNetCore.Mvc;
using SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;

namespace SmallsOnline.Web.Api.Controllers;

[ApiController]
[Route("api/favoriteTracks")]
public class FavoriteTracksController : ControllerBase
{
    private readonly ILogger<FavoriteTracksController> _logger;
    private readonly ICosmosDbService _cosmosDbService;

    public FavoriteTracksController(ILogger<FavoriteTracksController> logger, ICosmosDbService cosmosDbService)
    {
        _logger = logger;
        _cosmosDbService = cosmosDbService;
    }

    [HttpGet("{year}", Name = "GetFavoriteTracks")]
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