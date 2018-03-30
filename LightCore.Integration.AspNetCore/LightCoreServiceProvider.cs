using Microsoft.Extensions.DependencyInjection;
using System;

namespace LightCore.Integration.AspNetCore
{
    public class LightCoreServiceProvider : IServiceProvider, ISupportRequiredService
    {
        private readonly IContainer _container;

        internal LightCoreServiceProvider(IContainer container)
        {
            _container = container;
        }

        public object GetRequiredService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }


    public class LightCoreServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly Action<ContainerBuilder> _configurationAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacServiceProviderFactory"/> class.
        /// </summary>
        /// <param name="configurationAction">Action on a <see cref="ContainerBuilder"/> that adds component registrations to the conatiner.</param>
        public LightCoreServiceProviderFactory(Action<ContainerBuilder> configurationAction = null)
        {
            _configurationAction = configurationAction ?? (builder => { });
        }

        /// <summary>
        /// Creates a container builder from an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <param name="services">The collection of services</param>
        /// <returns>A container builder that can be used to create an <see cref="T:System.IServiceProvider" />.</returns>
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);

            _configurationAction(builder);

            return builder;
        }

        /// <summary>
        /// Creates an <see cref="T:System.IServiceProvider" /> from the container builder.
        /// </summary>
        /// <param name="containerBuilder">The container builder</param>
        /// <returns>An <see cref="T:System.IServiceProvider" /></returns>
        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            if (containerBuilder == null) throw new ArgumentNullException(nameof(containerBuilder));

            var container = containerBuilder.Build();

            return new LightCoreServiceProvider(container);
        }
    }
}