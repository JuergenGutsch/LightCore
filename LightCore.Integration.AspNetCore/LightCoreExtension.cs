using System;
using LightCore.Lifecycle;
using Microsoft.Extensions.DependencyInjection;

namespace LightCore.Integration.AspNetCore
{
    public static class LightCoreExtension
    {
        public static IServiceProvider GetServiceProvider(this IContainer container)
        {
            return new LightCoreServiceProvider(container);
        }

        public static void Populate(this IContainerBuilder builder, IServiceCollection services)
        {
            builder.Register<IServiceProvider>(container => container.GetServiceProvider())
                .ControlledBy<SingletonLifecycle>();

            foreach (var serviceDescriptor in services)
            {
                switch (serviceDescriptor.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        Register<SingletonLifecycle>(builder, serviceDescriptor);
                        break;
                    case ServiceLifetime.Scoped:
                        Register<ThreadSingletonLifecycle>(builder, serviceDescriptor);
                        break;
                    case ServiceLifetime.Transient:
                        Register<TransientLifecycle>(builder, serviceDescriptor);
                        break;
                }

            }
        }

        private static void Register<T>(IContainerBuilder builder, ServiceDescriptor serviceDescriptor) where T: ILifecycle, new()
        {
            if (serviceDescriptor.ImplementationFactory != null)
            {
                builder.Register(serviceDescriptor.ImplementationFactory)
                    .ControlledBy<T>();
            }
            else if (serviceDescriptor.ImplementationInstance != null)
            {
                builder.Register(serviceDescriptor.ImplementationInstance)
                    .ControlledBy<T>();
            }
            else
            {
                builder.Register(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType)
                    .ControlledBy<T>();
            }
        }
    }
}
