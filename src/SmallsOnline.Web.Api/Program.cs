namespace SmallsOnline.Web.Api;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables();

            Environment.SetEnvironmentVariable("CosmosDbConnectionString", builder.Configuration.GetValue<string>("CosmosDbConnectionString"));
            Environment.SetEnvironmentVariable("CosmosDbContainerName", builder.Configuration.GetValue<string>("CosmosDbContainerName"));
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}