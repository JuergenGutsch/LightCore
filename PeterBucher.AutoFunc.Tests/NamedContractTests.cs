using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class NamedContractTests
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