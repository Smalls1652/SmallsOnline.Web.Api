using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

/// <summary>
/// Service for interacting with Cosmos DB.
/// </summary>
public partial class CosmosDbService : ICosmosDbService
{
    public CosmosDbService()
    {
        cosmosDbClient = InitService(jsonSerializer);
    }

    /// <summary>
    /// The CosmosDB client.
    /// </summary>
    private CosmosClient cosmosDbClient;

    /// <summary>
    /// The JSON serializer for the CosmosDB client.
    /// </summary>
    private readonly CosmosDbSerializer jsonSerializer = new(
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = {
                new JsonDateTimeOffsetConverter()
            }
        }
    );

    /// <summary>
    /// Create a CosmosDB client.
    /// </summary>
    /// <param name="dbSerializer">The JSON serializer to use for the CosmosDB client.</param>
    /// <returns>A CosmosDB client.</returns>
    private static CosmosClient InitService(CosmosDbSerializer dbSerializer)
    {
        return new(
            connectionString: AppSettings.GetSetting("CosmosDbConnectionString"),
            clientOptions: new()
            {
                Serializer = dbSerializer
            }
        );
    }
}