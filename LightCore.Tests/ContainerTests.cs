using LightCore.Builder;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightCore.Tests
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