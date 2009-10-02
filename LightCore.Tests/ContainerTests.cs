using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeterBucher.AutoFunc.Builder;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void ContainerBuilder_can_initialize_controller()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            Assert.IsNotNull(container);
        }
    }
}