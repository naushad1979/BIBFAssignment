using Account.API;
using AccountAPI;
using ProductAPI.Exceptions;
using ProductAPI.Middlewares;
using Serilog;
using Serilog.Formatting.Json;

try
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureLogging();
    builder.Host.UseSerilog();

    //build JWT configuration
    //builder.Services.AddOptions<JWTOptions>().BindConfiguration(JWTOptions.SectionName);

    // Add services to the container.
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    ServicesRegistration.ConfigureServices(builder.Services, builder.Configuration);
    builder.Services.AddAuthorization();

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    //Add Global Error handler
    app.AddGlobalErrorHandler();

    app.UseSwagger();
    app.UseSwaggerUI();

    ServicesRegistration.ConfigureApps(app, app.Configuration);

    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllers();

    app.Run();

    void ConfigureLogging()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            //.Enrich.WithExceptionDetails()
            //.WriteTo.Debug()
            .WriteTo.Console(new JsonFormatter())
            .WriteTo.File(new JsonFormatter(), "log.txt")
            //.WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}
catch (Exception ex)
{
    throw ex;
}

public partial class Program
{

}
