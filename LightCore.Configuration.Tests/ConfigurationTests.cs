using System;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LightCore.Configuration.Tests
{
    public class ConfigurationTests
    {
        [Fact]
        public void Can_register_and_use_generics_with_configuration_api()
        {
            var configuration = new LightCoreConfiguration();
            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                // LightCore.TestTypes.IRepository`1, LightCore.TestTypes
                                                ContractType = typeof (IRepository<>).AssemblyQualifiedName,
                                                ImplementationType = typeof (Repository<>).AssemblyQualifiedName
                                            }
                                    };

            configuration.Registrations = registrations;

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var repository = container.Resolve<IRepository<Foo>>();

            repository.Should().NotBeNull();
            repository.Should().BeOfType<Repository<Foo>>();
        }

        [Fact]
        public void Set_lifecycle_on_fluent_registration_to_not_implementing_type_throws_argumentexception()
        {
            var builder = new ContainerBuilder();

            Action act = () => builder.Register<IFoo, Foo>().ControlledBy(typeof(object));

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
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

            bar.Should().NotBeNull();
        }

        [Fact]
        public void Can_configure_and_resolve_explicite_type_registration()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
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

            bar.Should().NotBeNull();
        }

        [Fact]
        public void Can_configure_and_resolve_type_registration_with_alias()
        {
            var configuration = new LightCoreConfiguration();

            configuration.TypeAliases.Add(new TypeAlias { Alias = "IBar", Type = typeof(IBar).AssemblyQualifiedName });

            configuration.TypeAliases.Add(new TypeAlias { Alias = "Bar", Type = typeof(Bar).AssemblyQualifiedName });

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
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

            bar.Should().NotBeNull();
        }

        [Fact]
        public void Can_configure_and_resolve_with_default_transient_lifecycle()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                ContractType = typeof(IBar).AssemblyQualifiedName,
                                                ImplementationType = typeof(Bar).AssemblyQualifiedName
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

            bar.Should().BeSameAs(barTwo);
        }

        [Fact]
        public void Can_configure_and_resolve_with_registration_default_singleton_lifecycle()
        {
            var configuration = new LightCoreConfiguration();

            configuration.DefaultLifecycle = "Singleton";

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
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

            bar.Should().BeSameAs(barTwo);
        }

        [Fact]
        public void Can_set_type_alias_for_lifecycles()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
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

            bar.Should().NotBeSameAs(barTwo);
        }

        [Fact]
        public void Can_set_lifecycle_full_qualified()
        {
            var configuration = new LightCoreConfiguration();

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
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

            bar.Should().NotBeSameAs(barTwo);
        }

        [Fact]
        public void RegistrationLoader_throws_argument_exception_on_missing_implementationtype()
        {
            var builder = new ContainerBuilder();
            var configuration = new LightCoreConfiguration();
            configuration.Registrations.Add(new Registration { ContractType = typeof(object).AssemblyQualifiedName });

            Action act = () => RegistrationLoader.Instance.Register(builder, configuration);

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void RegistrationLoader_throws_argument_exception_on_missing_contracttype()
        {
            var builder = new ContainerBuilder();
            var configuration = new LightCoreConfiguration();
            configuration.Registrations.Add(new Registration { ImplementationType = typeof(object).AssemblyQualifiedName });

            Action act = () => RegistrationLoader.Instance.Register(builder, configuration);

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Usage_of_global_registrations_and_grouped_registrations_work_together()
        {
            var configuration = new LightCoreConfiguration {ActiveRegistrationGroups = "Test"};

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

            objectInstance.Should().NotBeNull();
            barInstance.Should().NotBeNull();
        }
    }
}