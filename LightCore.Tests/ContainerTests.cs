using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void ContainerBuilder_can_initialize_controller()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            Assert.IsNotNull(container);
        }
    }
}