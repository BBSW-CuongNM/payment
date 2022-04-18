using Carter.OpenApi;
using Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Ultils.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        //  services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Description = "Payment service",
                Title = "Payment API",
                Version = "v1",
            });

            //options.DocInclusionPredicate((s, description) =>
            //{
            //    foreach (var metaData in description.ActionDescriptor.EndpointMetadata)
            //    {
            //        if (metaData is IIncludeOpenApi)
            //        {
            //            return true;
            //        }
            //    }
            //    return false;
            //});
        });

        return services;
    }

    public static IServiceCollection AddSqlServerDatabase<TContext>(this IServiceCollection services,
         string connectionString)
         where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseSqlServer(connectionString, c =>
            {
                c.CommandTimeout(10000);
                c.EnableRetryOnFailure(5);
            });
        });
        return services;
    }

    public static IServiceCollection AddPostgreSQLDatabase<TContext>(this IServiceCollection services,
         string connectionString)
         where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseNpgsql(connectionString, c =>
            {
                c.CommandTimeout(10000);
                c.EnableRetryOnFailure(5);
            });
        });
        return services;
    }
    public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.SaveToken = true;
            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                ValidAudience = configuration.GetSection("Jwt:Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value))
            };
        });
        return services;
    }
}