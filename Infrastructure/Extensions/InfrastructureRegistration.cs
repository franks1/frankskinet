using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
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
        }
    }
}