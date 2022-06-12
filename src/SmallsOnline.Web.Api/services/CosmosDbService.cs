using Microsoft.Azure.Cosmos;
using SmallsOnline.Web.Api.Helpers;

namespace SmallsOnline.Web.Api.Services;

public partial class CosmosDbService : ICosmosDbService
{
    public CosmosDbService()
    {
        cosmosDbClient = InitService(jsonSerializer);
    }

    private CosmosClient cosmosDbClient;
    private readonly CosmosDbSerializer jsonSerializer = new(
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = {
                new JsonDateTimeOffsetConverter()
            }
        }
    );

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