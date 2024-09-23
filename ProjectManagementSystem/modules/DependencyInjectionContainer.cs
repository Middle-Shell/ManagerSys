using Microsoft.Extensions.DependencyInjection;

namespace ProjectManagementSystem.modules
{
    public static class DependencyInjectionContainer
    {
        private static IServiceProvider serviceProvider;

        public static void Initialize(IServiceCollection services)
        {
            serviceProvider = services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}