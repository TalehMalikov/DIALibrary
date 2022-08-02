using Core.CrossCuttingConcerns.Caching.MicrosoftMemoryCache;
using Library.Core.CrossCuttingConcerns.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
