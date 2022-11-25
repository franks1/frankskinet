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
            services.AddSwaggerGen((gen) =>
            {
                gen.SwaggerDoc("v1", new OpenApiInfo() { Title = "Skinet Api", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authentication Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                
                gen.AddSecurityDefinition("Bearer", securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement { { securityScheme, new[] { "Bearer" } } };
    
                gen.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
    }
}