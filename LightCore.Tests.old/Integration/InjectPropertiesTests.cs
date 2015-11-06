using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class InjectPropertiesTests
    {
        [Test]
        public void Container_can_inject_properties()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var foo = new Foo();
            container.InjectProperties(foo);

            Assert.IsNotNull(foo.Bar);
        }
    }
}