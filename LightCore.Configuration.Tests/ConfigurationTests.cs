using System;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Configuration.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void Enabled_or_disable_registrations_works()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>()
                                    {
                                        new Registration
                                            {
                                                ContractType = typeof(IBar).AssemblyQualifiedName,
                                                ImplementationType = typeof(Bar).AssemblyQualifiedName,
                                                Enabled = "false"
                                            }
                                    };

            configuration.Registrations = registrations;

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            Assert.NotNull(bar);
        }

        [Test]
        public void Can_configure_and_resolve_explicite_type_registration()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = typeof(IBar).AssemblyQualifiedName,
                                                ImplementationType = typeof(Bar).AssemblyQualifiedName,
                                            }
                                    };

            configuration.RegistrationGroups.Add(new RegistrationGroup()
                                                     {
                                                         Registrations = registrations
                                                     });

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            Assert.NotNull(bar);
        }

        [Test]
        public void Can_configure_and_resolve_type_registration_with_alias()
        {
            var configuration = new LightCoreConfiguration();

            configuration.TypeAliases.Add(new TypeAlias { Alias = "IBar", Type = typeof(IBar).AssemblyQualifiedName });

            configuration.TypeAliases.Add(new TypeAlias { Alias = "Bar", Type = typeof(Bar).AssemblyQualifiedName });

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = "IBar",
                                                ImplementationType = "Bar",
                                            }
                                    };

            configuration.RegistrationGroups.Add(new RegistrationGroup()
            {
                Registrations = registrations
            });

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            Assert.NotNull(bar);
        }

        [Test]
        public void Can_configure_and_resolve_with_default_transient_lifecycle()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = typeof(IBar).AssemblyQualifiedName,
                                                ImplementationType = typeof(Bar).AssemblyQualifiedName,
                                            }
                                    };

            configuration.RegistrationGroups.Add(new RegistrationGroup()
            {
                Registrations = registrations
            });

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();
            var barTwo = container.Resolve<IBar>();

            Assert.AreNotSame(bar, barTwo);
        }

        [Test]
        public void Can_configure_and_resolve_with_registration_default_singleton_lifecycle()
        {
            var configuration = new LightCoreConfiguration();

            configuration.DefaultLifecycle = "Singleton";

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = typeof(IBar).AssemblyQualifiedName,
                                                ImplementationType = typeof(Bar).AssemblyQualifiedName,
                                            }
                                    };

            configuration.RegistrationGroups.Add(new RegistrationGroup()
            {
                Registrations = registrations
            });

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();
            var barTwo = container.Resolve<IBar>();

            Assert.AreSame(bar, barTwo);
        }

        [Test]
        public void Can_set_type_alias_for_lifecycles()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = typeof(IBar).AssemblyQualifiedName,
                                                ImplementationType = typeof(Bar).AssemblyQualifiedName,
                                                Lifecycle = "Transient"
                                            }
                                    };

            configuration.RegistrationGroups.Add(new RegistrationGroup()
            {
                Registrations = registrations
            });

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();
            var barTwo = container.Resolve<IBar>();

            Assert.AreNotSame(bar, barTwo);
        }

        [Test]
        public void Can_set_lifecycle_full_qualified()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = typeof(IBar).AssemblyQualifiedName,
                                                ImplementationType = typeof(Bar).AssemblyQualifiedName,
                                                Lifecycle = typeof(TransientLifecycle).AssemblyQualifiedName
                                            }
                                    };

            configuration.RegistrationGroups.Add(new RegistrationGroup()
            {
                Registrations = registrations
            });

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();
            var barTwo = container.Resolve<IBar>();

            Assert.AreNotSame(bar, barTwo);
        }

        [Test]
        public void Usage_of_global_registrations_and_grouped_registrations_work_together()
        {
            var configuration = new LightCoreConfiguration();
            configuration.ActiveRegistrationGroups = "Test";

            var globalRegistrations = new List<Registration>()
                                    {
                                        new Registration
                                            {
                                                ContractType = typeof(object).AssemblyQualifiedName,
                                                ImplementationType = typeof(object).AssemblyQualifiedName
                                            }
                                    };

            var groupRegistrations = new List<Registration>
                                         {
                                             new Registration
                                                 {
                                                     ContractType = typeof(IBar).AssemblyQualifiedName,
                                                     ImplementationType = typeof(Bar).AssemblyQualifiedName
                                                 }
                                         };

            configuration.Registrations = globalRegistrations;
            configuration.RegistrationGroups = new List<RegistrationGroup>
                                                   {
                                                       new RegistrationGroup
                                                           {
                                                               Name = "Test",
                                                               Registrations = groupRegistrations
                                                           }
                                                   };

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var objectInstance = container.Resolve<object>();
            var barInstance = container.Resolve<IBar>();

            Assert.NotNull(objectInstance);
            Assert.NotNull(barInstance);
        }
    }
}