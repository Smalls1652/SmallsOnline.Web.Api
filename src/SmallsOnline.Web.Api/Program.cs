using Microsoft.Extensions.DependencyInjection;

using SmallsOnline.Web.Api.Services;

namespace SmallsOnline.Web.Api;

public class Program
{
    public static void Main()
    {
        IHost host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(
                services => {
                    services.AddSingleton<ICosmosDbService, CosmosDbService>();
                }
            )
            .Build();

        host.Run();
    }
}
