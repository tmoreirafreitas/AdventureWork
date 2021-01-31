using AdventureWork.Infra.CrossCutting.IoC.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWork.Infra.CrossCutting.IoC
{
    public class RepositoryAndServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.UseRepositoriesAndServices();
        }
    }
}
