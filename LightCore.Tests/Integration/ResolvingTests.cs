using FluentAssertions;
using FluentAssertions.Common;
using LightCore.TestTypes;
using Xunit;


namespace LightCore.Tests.Integration
{
    /// <summary>
    /// Summary description for ResolvingTests
    /// </summary>
    
    public class ResolvingTests
    {
        [Fact]
        public void Foo_is_resolved_with_default_constructor_if_no_bar_is_registered()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();
        }

        [Fact]
        public void Foo_is_resolved_with_the_bar_only_constructor_if_bar_is_registered()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var actual = container.Resolve<IFoo>();

            actual.Should().NotBeNull();
            actual.Bar.Should().NotBeNull();
        }

        [Fact]
        public void Foo_is_resolved_with_the_bar_with_arguments_constructor_if_bar_registered_and_arguments_supported()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments("Test", true);
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();

            //Assert.IsNotNull(instance);
            //Assert.IsNotNull(instance.Bar);
            //Assert.IsNotNull((instance as Foo).Arg1);
            //Assert.AreEqual(true, (instance as Foo).Arg2);
        }

        [Fact]
        public void Foo_is_resolved_with_bool_argument_constructor_if_only_that_argument_is_supported()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments(true);

            var container = builder.Build();

            var actual = container.Resolve<IFoo>() as Foo;

            actual.Should().NotBeNull();
            actual.Arg2.Should().BeTrue();
        }

        [Fact]
        public void Can_resolve_dependencies_with_injected_container()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar>(c => new Bar());

            var container = builder.Build();

            var resolvedContainer = container.Resolve<IContainer>();
            var actual = resolvedContainer.Resolve<IBar>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Bar>();

            resolvedContainer.Should().NotBeNull();
            resolvedContainer.Should().BeAssignableTo<IContainer>();
        }

        [Fact]
        public void Container_resolves_registered_interface_registration()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();
            var actual = container.Resolve<IFoo>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Foo>();
        }

        [Fact]
        public void Container_resolves_registed_abstract_class()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<FooBase, Foo>();

            var container = builder.Build();
            var actual = container.Resolve<FooBase>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Foo>();
        }

        [Fact]
        public void Container_resolves_registered_class_mapped_to_itself()
        {
            var builder = new ContainerBuilder();
            builder.Register<Bar>();

            var container = builder.Build();
            var actual = container.Resolve<Bar>();

            actual.Should().NotBeNull();
        }

        [Fact]
        public void Container_resolves_a_whole_object_tree()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var actual = container.Resolve<IFoo>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Foo>();
            actual.Bar.Should().NotBeNull();
        }
    }
}