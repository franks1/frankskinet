using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureRegistration
    {
       public static void  InfrastructureServiceRegistration(this IServiceCollection service, IConfiguration configuration){
          service.AddDbContext<StoreContext>((option)=>{
                option.UseSqlite(configuration.GetConnectionString("appconnection"));
          });
          
          service.AddDbContext<AppIdentityDbContext>((option)=>{
              option.UseSqlite(configuration.GetConnectionString("identityconnection"));
          });
          
          
        service.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        service.AddScoped<IProductRepository,ProductRepository>();
        service.AddScoped<IBasketRepository, BasketRepository>();

       }
    }
}