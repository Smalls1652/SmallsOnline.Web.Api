using SmallsOnline.Web.Lib.Models.FavoritesOf.Tracks;
using SmallsOnline.Web.Api.Services;

namespace SmallsOnline.Web.Api.Functions;

public class GetFavoriteTracks
{
    private readonly ILogger _logger;
    private readonly ICosmosDbService _cosmosDbService;

    public GetFavoriteTracks(ILoggerFactory loggerFactory, ICosmosDbService cosmosDbService)
    {
        _logger = loggerFactory.CreateLogger<GetFavoriteTracks>();
        _cosmosDbService = cosmosDbService;
    }

    [Function("GetFavoriteTracks")]
    public HttpResponseData Run(
        [HttpTrigger(
            authLevel: AuthorizationLevel.Anonymous,
            methods: "get",
            Route = "favoriteTracks/{year}"
        )]
        HttpRequestData httpReq,
        string year
    )
    {
        _logger.LogInformation($"Trigger for '{GetType().Name}' received.");

        List<TrackData> retrievedAlbums = _cosmosDbService.GetFavoriteTracksOfYear(
            listYear: year
        );

        string tracksJson = JsonSerializer.Serialize(retrievedAlbums);

        HttpResponseData httpRsp = httpReq.CreateResponse(
            statusCode: HttpStatusCode.OK
        );

        httpRsp.Headers.Add(
            name: "Content-Type",
            "application/json"
        );

        httpRsp.WriteString(tracksJson);

        return httpRsp;
    }
}