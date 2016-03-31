using System;
using Microsoft.Extensions.DependencyInjection;

namespace LightCore.Integration.AspNetCore
{
    public class LightCoreServiceScope : IServiceScope
    {
        public LightCoreServiceScope(IContainer container)
        {
            ServiceProvider = container.Resolve<IServiceProvider>();
        }

        public void Dispose()
        {
        }

        public IServiceProvider ServiceProvider { get; }
    }
}