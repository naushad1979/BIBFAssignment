using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Account.API.Infrastructure;
using AccountAPI.Services;
using AccountAPI.Infrastructure.Repository;
using ProductAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

            string callingAssemblyName = Assembly.GetEntryAssembly().ManifestModule.Name.ToUpper();
            if (!callingAssemblyName.Contains("TESTHOST"))
            {
                // migrate any database changes on startup (includes initial db creation)
                var dataContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                dataContext.Database.Migrate();

                //var premiumContext = serviceProvider.GetRequiredService<PremiumContext>();
                // PremiumContextSeed.SeedAsync(premiumContext);

                // Configure the HTTP request pipeline.
                //if (app.Environment.IsDevelopment())
                //{
                // app.UseSwagger();
                // app.UseSwaggerUI();
                // }
            }

            app.UseCors("CorsPolicy");

            //app.UseAuthorization();

            //app.MapControllers();

            //app.Run();

            return app;
        }
    }
   
}
