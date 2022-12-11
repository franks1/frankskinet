using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Servces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddEndpointsApiExplorer();
            services.Configure<ApiBehaviorOptions>((option) =>
            {
                option.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(f => f.Value.Errors.Count > 0)
                    .SelectMany(a => a.Value.Errors)
                    .Select(a => a.ErrorMessage).ToArray();

                    var validationErrors = new ApiValidationResponse { Errors = errors };
                    return new BadRequestObjectResult(validationErrors);
                };
            });

            return services;
        }
    }
}