using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.modules;
using ProjectManagementSystem.modules.authentication;
using ProjectManagementSystem.modules.ControlData;
using ProjectManagementSystem.modules.ControlData.Interfaces;
using ProjectManagementSystem.modules.UserInteraction;

namespace ProjectManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IDataStorage, JsonDataStorage>(); 
            serviceCollection.AddSingleton<DataManager>();
            serviceCollection.AddSingleton<Authenticator>();
            serviceCollection.AddSingleton<Logger>(); 
            serviceCollection.AddScoped<CLI>();

            DependencyInjectionContainer.Initialize(serviceCollection);

            var cli = DependencyInjectionContainer.GetService<CLI>();

            cli.Run();
        }
    }
}