using AdventureWork.Infra.CrossCutting.IoC.Extensions;
using AdventureWork.Infra.CrossCutting.MiddlewareFilterNotification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWork.Infra.CrossCutting.IoC
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.UseRepositoriesAndServices();
            services.AddScoped<NotificationContext>();
        }
    }
}
