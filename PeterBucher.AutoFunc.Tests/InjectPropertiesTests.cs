using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeterBucher.AutoFunc.Builder;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests
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