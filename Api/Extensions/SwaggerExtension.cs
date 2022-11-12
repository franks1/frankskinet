using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(a => a.SwaggerDoc("v1", new OpenApiInfo { Title = "Skinet Api", Version = "v1" }));
            return services;
        }
    }
}