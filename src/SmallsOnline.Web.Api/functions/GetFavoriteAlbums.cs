using SmallsOnline.Web.Api.Lib.Models.Albums;
using SmallsOnline.Web.Api.Services;

namespace SmallsOnline.Web.Api.Functions;

public class GetFavoriteAlbums
{
    private readonly ILogger _logger;
    private readonly ICosmosDbService _cosmosDbService;

    public GetFavoriteAlbums(ILoggerFactory loggerFactory, ICosmosDbService cosmosDbService)
    {
        _logger = loggerFactory.CreateLogger<GetFavoriteAlbums>();
        _cosmosDbService = cosmosDbService;
    }

    [Function("GetFavoriteAlbums")]
    public HttpResponseData Run(
        [HttpTrigger(
            authLevel: AuthorizationLevel.Anonymous,
            methods: "get",
            Route = "favoriteAlbums/{year}"
        )]
        HttpRequestData httpReq,
        string year
    )
    {
        _logger.LogInformation($"Trigger for '{GetType().Name}' received.");

        List<AlbumData> retrievedAlbums = _cosmosDbService.GetFavoriteAlbumsOfYear(
            listYear: year
        );

        retrievedAlbums.Sort(
            (AlbumData album1, AlbumData album2) => album2.IsBest.CompareTo(album1.IsBest)
        );

        string albumsJson = JsonSerializer.Serialize(retrievedAlbums);

        HttpResponseData httpRsp = httpReq.CreateResponse(
            statusCode: HttpStatusCode.OK
        );

        httpRsp.Headers.Add(
            name: "Content-Type",
            "application/json"
        );

        httpRsp.WriteString(albumsJson);

        return httpRsp;
    }
}