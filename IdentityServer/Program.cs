using IdentityServer;
using IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

//build JWT configuration
builder.Services.AddOptions<JWTOptions>().BindConfiguration(JWTOptions.SectionName);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
