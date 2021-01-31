using AdventureWork.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace AdventureWork.Infra.CrossCutting.IoC
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringsSetting>(configuration.GetSection("ConnectionStringsSetting"));
            services.AddScoped<IDbConnection, SqlConnection>();
            services.AddScoped<IDatabaseContextFactory, DatabaseContextFactory>();
            services.AddScoped<IDatabaseContext, DatabaseContext>();            
        }
    }
}
