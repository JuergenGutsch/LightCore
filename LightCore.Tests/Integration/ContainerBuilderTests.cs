using System;
using FluentAssertions;
using LightCore.Lifecycle;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Integration
{
    public class ContainerBuilderTests
    {
        [Fact]
        public void ContainerBuilder_with_registered_groups_reconice_these()
        {
            var builder = new ContainerBuilder
            {
                ActiveRegistrationGroups = "Test"
            };


            builder.Register<IFoo, FooTwo>().WithGroup("Test");
            builder.Register<IFoo, Foo>().WithGroup("Test2");

            var container = builder.Build();

            container.Resolve<IFoo>().Should().BeOfType<FooTwo>();
        }

        [Fact]
        public void ContainerBuilder_with_non_active_groups_throws_registrationNotFoundException()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, FooTwo>().WithGroup("Test");
            builder.Register<IFoo, Foo>().WithGroup("Test2");

            var container = builder.Build();

            Action act = () => container.Resolve<IFoo>();

            act.ShouldThrow<RegistrationNotFoundException>();
        }

        [Fact]
        public void ContainerBuilder_can_register_generic_type()
        {
            var builder = new ContainerBuilder();

            builder.Register(typeof (IRepository<>), typeof (Repository<>));
        }

        [Fact]
        public void ContainerBuilder_throws_exception_on_interface_to_interface_registration()
        {
            var builder = new ContainerBuilder();

            Action act = () => builder.Register<IFoo>();

            act.ShouldThrow<InvalidRegistrationException>();
        }

        [Fact]
        public void ContainerBuilder_can_register_types()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>();
        }

        [Fact]
        public void ContainerBuilder_throws_on_not_assignable_contract_to_implementation()
        {
            IContainer container = null;
            Action act = () =>
            {
                var builder = new ContainerBuilder();
                builder.Register(typeof (IFoo), typeof (Bar));

                container = builder.Build();
            };

            act.ShouldThrow<ContractNotImplementedByTypeException>();
        }

        [Fact]
        public void ContainerBuilder_can_register_activation_functions()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo>(c => new Foo());

            builder.Build();
        }

        [Fact]
        public void ContainerBuilder_can_register_type_to_self()
        {
            var builder = new ContainerBuilder();

            builder.Register<Foo>();

            builder.Build();
        }

        [Fact]
        public void ContainerBuilders_default_scope_is_transient()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instanceOne = container.Resolve<IFoo>();
            var instanceTwo = container.Resolve<IFoo>();

            instanceOne.Should().NotBeSameAs(instanceTwo);
        }

        [Fact]
        public void CointainerBuilder_default_scope_can_be_altered_to_singleton()
        {
            var builder = new ContainerBuilder();

            builder.DefaultControlledBy<SingletonLifecycle>();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instanceOne = container.Resolve<IFoo>();
            var instanceTwo = container.Resolve<IFoo>();

            instanceOne.Should().BeSameAs(instanceTwo);
        }
    }
}