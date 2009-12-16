using System;

using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Configuration.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void Can_configure_and_resolve_explicite_type_registration()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                                ImplementationType = "LightCore.TestTypes.Bar, LightCore.TestTypes",
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

            configuration.TypeAliases.Add(new TypeAlias { Alias = "IBar", Type = "LightCore.TestTypes.IBar, LightCore.TestTypes" });

            configuration.TypeAliases.Add(new TypeAlias { Alias = "Bar", Type = "LightCore.TestTypes.Bar, LightCore.TestTypes" });

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
                                                ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                                ImplementationType = "LightCore.TestTypes.Bar, LightCore.TestTypes",
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
                                                ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                                ImplementationType = "LightCore.TestTypes.Bar, LightCore.TestTypes",
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
                                                ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                                ImplementationType = "LightCore.TestTypes.Bar, LightCore.TestTypes",
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
                                                ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                                ImplementationType = "LightCore.TestTypes.Bar, LightCore.TestTypes",
                                                Lifecycle = "LightCore.Lifecycle.TransientLifecycle, LightCore"
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
                                                ContractType = "System.Object",
                                                ImplementationType = "System.Object"
                                            }
                                    };

            var groupRegistrations = new List<Registration>
                                         {
                                             new Registration
                                                 {
                                                     ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                                     ImplementationType = "LightCore.TestTypes.Bar, LightCore.TestTypes"
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