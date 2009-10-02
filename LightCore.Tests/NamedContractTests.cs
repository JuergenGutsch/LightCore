using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class NamedContractTests
    {
        [TestMethod]
        public void Container_can_resolve_named_registration()
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