using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class InjectPropertiesTests
    {
        [TestMethod]
        public void Inject_properties_works()
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