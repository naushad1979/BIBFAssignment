using Account.API.Infrastructure;
using AccountAPI.Infrastructure.Repository;
using AccountAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductAPI.Infrastructure.Repository;
using System.Text;

namespace Account.API
{
    public static class ServicesRegistration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(opts =>
            {
                var connString = configuration.GetConnectionString("ConnectionString");
                opts.UseSqlServer(connString, options =>
                {
                    options.MigrationsAssembly("ProductAPI");
                });
            });

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddFluentValidationAutoValidation();

            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    ValidAudience = configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            return services;
        }

        public static WebApplication ConfigureApps(WebApplication app, IConfiguration configuration)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // migrate any database changes on startup (includes initial db creation)
            var dataContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            dataContext.Database.Migrate();

            app.UseCors("CorsPolicy");
            return app;
        }
    }

}
