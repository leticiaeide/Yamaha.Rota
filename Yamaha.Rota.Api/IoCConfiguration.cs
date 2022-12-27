using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using Yamaha.Rota.CrossCutting;
using Yamaha.Rota.Data.Repositories;

namespace Yamaha.Rota.Api
{
    public class IoCConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbSession>();

            Bootstraper.RegisterService(services, configuration);
        }
    }
}
