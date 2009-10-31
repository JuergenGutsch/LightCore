using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class ResolvingNamedTests
    {
        [Test]
        public void Container_can_resolve_named_registration()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();
            builder.Register<IFoo, Foo>().WithName("test");

            var container = builder.Build();

            var namedInstance = container
                .Resolve<IFoo>("test");

            Assert.IsNotNull(namedInstance);
        }
    }
}