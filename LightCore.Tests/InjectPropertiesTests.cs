using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class InjectPropertiesTests
    {
        [Test]
        public void Container_can_inject_properties()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();
            
            var bar = new Bar();
            container.InjectProperties(bar);

            Assert.IsNotNull(bar.Foo);
        }
    }
}