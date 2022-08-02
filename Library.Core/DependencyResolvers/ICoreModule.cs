using Microsoft.Extensions.DependencyInjection;

namespace Library.Core.DependencyResolvers
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
