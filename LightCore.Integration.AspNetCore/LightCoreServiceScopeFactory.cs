using Microsoft.Extensions.DependencyInjection;

namespace LightCore.Integration.AspNetCore
{
    public class LightCoreServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IContainer _container;

        public LightCoreServiceScopeFactory(IContainer container)
        {
            _container = container;
        }

        public IServiceScope CreateScope()
        {
            return new LightCoreServiceScope(_container);
        }
    }
}