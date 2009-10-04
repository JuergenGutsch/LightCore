using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightCore.Tests
{
    [TestClass]
    public class ResolvingNamedTests
    {
        [TestMethod]
        public void Container_can_resolve_named_registration()
        {
            var builder = new ContainerBuilder();
            builder.Register<ILogger, Logger>();
            builder.Register<ILogger, NullLogger>().WithName("MyNullLogger");

            var container = builder.Build();

            var namedInstance = container.Resolve<ILogger>("MyNullLogger");

            Assert.IsNotNull(namedInstance);
        }
    }
}