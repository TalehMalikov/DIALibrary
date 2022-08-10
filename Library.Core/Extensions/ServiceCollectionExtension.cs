using Library.Core.DependencyResolvers;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }
            return ServiceHelper.CreateInstance(services);
        }
    }
}