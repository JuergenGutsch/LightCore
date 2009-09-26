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
            IContainer container = new Container();
            container.Register<ILogger, Logger>();
            container.Register<ILogger, NullLogger>().Named("MyNullLogger");

            var namedInstance = container.ResolveNamed<ILogger>("MyNullLogger");

            Assert.IsNotNull(namedInstance);
        }
    }
}