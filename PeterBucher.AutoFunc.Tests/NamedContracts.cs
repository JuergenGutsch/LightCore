using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Tests.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class NamedContracts
    {
        [TestMethod]
        public void Registering_named_service()
        {
            var builder = new ContainerBuilder();

            builder.Register<ILogger, Logger>();
            builder.Register<ILogger, NullLogger>().WithName("MyNullLogger");

            var container = builder.Build();

            var namedInstance = container.ResolveNamed<ILogger>("MyNullLogger");

            Assert.IsNotNull(namedInstance);
        }
    }
}