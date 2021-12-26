using SmallsOnline.Web.Lib.Models.Json;
using SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;
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

        string albumsJson = JsonSerializer.Serialize(retrievedAlbums, serializerOptions);

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