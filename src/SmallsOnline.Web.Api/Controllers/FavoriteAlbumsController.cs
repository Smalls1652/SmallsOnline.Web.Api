using Microsoft.AspNetCore.Mvc;
using SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;

namespace SmallsOnline.Web.Api.Controllers;

[ApiController]
[Route("api/favoriteAlbums")]
public class FavoriteAlbumsController : ControllerBase
{
    private readonly ILogger<FavoriteAlbumsController> _logger;
    private readonly ICosmosDbService _cosmosDbService;

    public FavoriteAlbumsController(ILogger<FavoriteAlbumsController> logger, ICosmosDbService cosmosDbService)
    {
        _logger = logger;
        _cosmosDbService = cosmosDbService;
    }

    [HttpGet("{year}" ,Name = "GetFavoriteAlbums")]
    public IEnumerable<AlbumData> GetFavoriteAlbums(string year)
    {
        _logger.LogInformation("Getting favorite albums for {year}.", year);
        List<AlbumData> retrievedAlbums = _cosmosDbService.GetFavoriteAlbumsOfYear(
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
}