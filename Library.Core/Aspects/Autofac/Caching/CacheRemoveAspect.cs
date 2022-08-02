using Castle.DynamicProxy;
using Library.Core.CrossCuttingConcerns.Caching;
using Library.Core.DependencyResolvers;
using Library.Core.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string _pattern;
        private readonly ICacheManager _cacheManager;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceHelper.ServiceProvider.GetService<ICacheManager>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
