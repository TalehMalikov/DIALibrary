using Microsoft.Extensions.DependencyInjection;

namespace Library.Core.DependencyResolvers
{
    public static class ServiceHelper
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IServiceCollection CreateInstance(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
