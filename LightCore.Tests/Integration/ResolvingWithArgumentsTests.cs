using FluentAssertions;
using LightCore.TestTypes;

using Xunit;

namespace LightCore.Tests.Integration
{
    
    public class ResolvingWithArgumentsTests
    {
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
        public void Container_resolves_instances_with_arguments()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();

            builder
                .Register<IFoo, Foo>()
                .WithArguments("Peter", true);

            var container = builder.Build();

            var actual = container.Resolve<IFoo>() as Foo;

            actual.Should().NotBeNull();
            actual.Arg1.Should().Be("Peter");
            actual.Arg2.Should().BeTrue();
        }

        [Fact]
        public void Container_resolves_instances_with_dependencies_and_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>().WithArguments("Peter", true);

            var container = builder.Build();

            var actual = container.Resolve<IFoo>() as Foo;

            actual.Should().NotBeNull();
            actual.Arg1.Should().Be("Peter");
            actual.Arg2.Should().BeTrue();
        }
    }
}