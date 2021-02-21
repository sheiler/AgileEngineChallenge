using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.ImageCache.Infrastructure.Contracts;
using TestTask.ImageCache.Infrastructure.Domain;
using TestTask.ImageCache.Infrastructure.Services;

namespace TestTask.ImageCache.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void UseHttp(this IServiceCollection services)
        {

            services.AddHttpClient<IAgileEngineClient, AgileEngineClient>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestTask.ImageCache.API", Version = "v1" });
            });
        }

        public static void UseCors(this IServiceCollection services, string cors)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: cors, builder => 
                    builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }
        public static void AddScopes(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICacheImage, MemoryCacheImage>();
        }
    }
}
