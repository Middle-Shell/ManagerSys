using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.modules.authentication;
using ProjectManagementSystem.modules.ControlData;
using ProjectManagementSystem.modules.ControlData.Interfaces;
using ProjectManagementSystem.modules.UserInteraction;

namespace ProjectManagementSystem.modules
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DataManager>();
            services.AddSingleton<IDataStorage, JsonDataStorage>();
            services.AddSingleton<Authenticator>();

            services.AddSingleton<Logger>();

            services.AddScoped<CLI>();
        }
    }
}