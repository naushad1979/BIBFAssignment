using Account.API;
using ProductAPI.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Formatting.Json;

try
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureLogging();
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    ServicesRegistration.ConfigureServices(builder.Services, builder.Configuration);
    builder.Services.AddAuthorization();

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    //Add Global Error handler
    app.UseGlobalErrorHandler();

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
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Filter.ByExcluding(Matching.FromSource("Microsoft"))
            .Filter.ByExcluding(Matching.FromSource("System"))
            .Enrich.FromLogContext()            
            .WriteTo.Console(new JsonFormatter())
            .WriteTo.File(new JsonFormatter(), "log.json")
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
