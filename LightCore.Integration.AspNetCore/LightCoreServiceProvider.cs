using System;

namespace LightCore.Integration.AspNetCore
{
    public class LightCoreServiceProvider : IServiceProvider
    {
        private readonly IContainer _container;

        internal LightCoreServiceProvider(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}