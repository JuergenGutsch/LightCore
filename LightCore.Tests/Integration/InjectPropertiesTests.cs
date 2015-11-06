using FluentAssertions;
using LightCore.TestTypes;

using Xunit;

namespace LightCore.Tests.Integration
{
    
    public class InjectPropertiesTests
    {
        [Fact]
        public void Container_can_inject_properties()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var foo = new Foo();
            container.InjectProperties(foo);

            foo.Bar.Should().NotBeNull();
        }
    }
}