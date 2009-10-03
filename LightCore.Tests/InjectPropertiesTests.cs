using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightCore.Tests
{
    [TestClass]
    public class InjectPropertiesTests
    {
        [TestMethod]
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