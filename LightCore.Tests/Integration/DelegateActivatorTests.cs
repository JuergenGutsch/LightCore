using FluentAssertions;
using LightCore.TestTypes;

using Xunit;

namespace LightCore.Tests.Integration
{
    
    public class DelegateActivatorTests
    {
        [Fact]
        public void DelegateActivator_can_return_an_instance_from_given_new_function()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo());

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();

            foo.Should().NotBeNull();
        }

        [Fact]
        public void DelegateActivator_can_return_new_object_with_default_transient_lifecycle()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo());

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            foo.Should().NotBeSameAs(foo2);
        }
    }
}