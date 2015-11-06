using System.Collections.Generic;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Integration
{

    public class ResolvingWithRuntimeArgumentsTests
    {
        [Fact]
        public void Container_Can_Resolve_With_Given_Runtime_Argument_Successfully()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var foo = (Foo)container.Resolve<IFoo>("yeah");

            foo.Should().NotBeNull();
            foo.Bar.Should().NotBeNull();
            foo.Arg1.Should().NotBeNull();
        }

        [Fact]
        public void Container_Does_Not_Reuse_Runtime_Arguments()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var foo = container.Resolve<IFoo>("yeah", true) as Foo;

            foo.Should().NotBeNull();
            foo.Arg1.Should().BeEquivalentTo("yeah");
            foo.Arg2.Should().BeTrue();
        }

        [Fact]
        public void Container_Can_Resolve_With_Named_Given_Runtime_Argument_Successfully()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var foo = container.Resolve<IFoo>(new Dictionary<string, object> { { "arg2", true }, { "arg1", "Peter" } }) as Foo;

            foo.Should().NotBeNull();
            foo.Arg1.Should().BeEquivalentTo("Peter");
            foo.Arg2.Should().BeTrue();
        }

        [Fact]
        public void Container_Can_Resolve_With_Anonymous_Named_Given_Runtime_Argument_Successfully()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var foo = container.Resolve<IFoo>(new AnonymousArgument(new { arg2 = true, arg1 = "Peter" })) as Foo;

            foo.Should().NotBeNull();
            foo.Arg1.Should().BeEquivalentTo("Peter");
            foo.Arg2.Should().BeTrue();
        }
    }
}