using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Yamaha.Rota.Domain.IoC;

namespace Yamaha.Rota.CrossCutting
{
    public static class Bootstraper
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.RegisterTypes(Module.GetTypes());

            services.RegisterTypes(Data.IoC.Module.GetTypes());
        }

        public static void RegisterTypes(this IServiceCollection container, Dictionary<Type, Type> types)
        {
            foreach (var item in types)
            {
                container.AddScoped(item.Key, item.Value);
            }
        }
    }
}