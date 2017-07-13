using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using LightCore.Lifecycle;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Integration
{
    public class ContainerTests
    {
        [Fact]
        public void Container_resolves_properties_in_transient_lifecycle()
        {
            var builder = new ContainerBuilder();
            builder.RegisterFactory<IDisposable>(c => new MemoryStream()).ControlledBy<TransientLifecycle>();
            builder.DefaultControlledBy<TransientLifecycle>();

            var container = builder.Build();
            var streamContainerOne = container.Resolve<StreamContainer>();
            var streamContainerTwo = container.Resolve<StreamContainer>();

            streamContainerOne.Should().NotBeSameAs(streamContainerTwo);
            streamContainerOne.Stream.Should().NotBeSameAs(streamContainerTwo.Stream);
        }

        [Fact]
        public void Container_throws_exception_on_singleresolve_with_multiple_registrations()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IFoo, FooTwo>();

            var container = builder.Build();
            
            Action act = () => container.Resolve<IFoo>();

            act.ShouldThrow<RegistrationNotFoundException>();
        }


        [Fact]
        public void Container_can_resolve_concrete_types()
        {
            var builder = new ContainerBuilder();

            var container = builder.Build();

            var foo = container.Resolve<Foo>();

            foo.Should().NotBeNull();
        }

        [Fact]
        public void Container_can_resolve_concrete_types_with_registered_dependencies()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var foo = container.Resolve<Foo>();

            foo.Bar.Should().NotBeNull();
        }

        [Fact]
        public void Container_can_resolve_with_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments(3);

            var container = builder.Build();

            var foo = container.Resolve<IFoo>() as Foo;

            foo.Arg3.Should().Be(3);
        }

        [Fact]
        public void Container_can_resolve_factories()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var fooFactory = container.Resolve<Func<IFoo>>();
            var fooOne = fooFactory();
            var fooTwo = fooFactory();

            fooFactory.Should().NotBeNull();
            fooFactory.Should().BeOfType<Func<IFoo>>();

            fooOne.Should().NotBeNull();
            fooOne.Should().BeAssignableTo<IFoo>();

            fooTwo.Should().NotBeNull();
            fooTwo.Should().BeAssignableTo<IFoo>();
        }

        [Fact]
        public void Container_can_resolve_factories_with_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var fooFactory = container.Resolve<Func<string, IFoo>>();

            const string expected = "Hello World";

            var instance = fooFactory(expected) as Foo;

            fooFactory.Should().NotBeNull();
            instance.Should().NotBeNull();
            instance.Arg1.Should().Be(expected);

            var fooFactoryTwo = container.Resolve<Func<string, bool, IFoo>>();
            var fooTwo = fooFactoryTwo("Peter", true) as Foo;

            fooFactoryTwo.Should().NotBeNull();
            fooTwo.Arg1.Should().Be("Peter");
            fooTwo.Arg2.Should().BeTrue();
        }

        [Fact]
        public void Container_can_resolve_factories_as_dependendy()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var fooFactoryConsumer = container.Resolve<FooFactoryConsumer>();

            fooFactoryConsumer.Should().NotBeNull();
            fooFactoryConsumer.Foo.Should().NotBeNull();
        }

        [Fact]
        public void Container_can_resolve_open_generic_types()
        {
            var builder = new ContainerBuilder();
            builder.Register(typeof(IRepository<>), typeof(Repository<>));

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<Foo>>();

            fooRepository.Should().NotBeNull();
            fooRepository.Should().BeOfType<Repository<Foo>>();
        }

        [Fact]
        public void Container_can_resolve_open_generic_types_with_few_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register(typeof(IRepository<,>), typeof(Repository<,>));

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<Foo, int>>();

            fooRepository.Should().NotBeNull();
            fooRepository.Should().BeOfType<Repository<Foo, int>>();
        }

        [Fact]
        public void Container_can_resolve_a_few_open_generic_types()
        {
            var builder = new ContainerBuilder();
            builder.Register(typeof(IRepository<>), typeof(Repository<>));

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<IFoo>>();
            var barRepository = container.Resolve<IRepository<IBar>>();

            fooRepository.Should().NotBeNull();
            barRepository.Should().NotBeNull();
        }

        [Fact]
        public void Container_can_resolve_types_to_self_automatically()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var resolvedInstance = container.Resolve<Foo>();

            resolvedInstance.Should().NotBeNull();
        }

        [Fact]
        public void IContainer_is_automatically_registered_when_container_was_build()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var actual = container.Resolve<IContainer>();

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<IContainer>();
        }

        [Fact]
        public void ContainerBuilder_can_initialize_container()
        {
            var builder = new ContainerBuilder();
            var actual = builder.Build();

            actual.Should().NotBeNull();
        }

        [Fact]
        public void Container_can_resolve_all_instances_based_on_a_predicate()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();

            builder.Register<IFoo, Foo>();
            builder.Register<IFoo, FooTwo>();

            var container = builder.Build();

            var actual = container.ResolveAll<IFoo>();

            actual.Count().Should().Be(2);
        }

        [Fact]
        public void Container_can_resolve_enumerables_of_contract()
        {
            var builder = new ContainerBuilder();

            builder.RegisterFactory<IBar>(c => new Bar());
            builder.RegisterFactory<IBar>(c => new Bar());

            var container = builder.Build();

            var actual = container.Resolve<EnumerableTest>();

            actual.Should().NotBeNull();
            actual.Bars.Count().Should().Be(2);
        }

        private class StreamContainer
        {
            public StreamContainer()
            {
            }

            public StreamContainer(IDisposable disposable)
            {
                Stream = disposable;
            }

            public IDisposable Stream { get; }
        }
    }
}