using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Tests.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    /// <summary>
    /// Summary description for RegisterTypesTests
    /// </summary>
    [TestClass]
    public class RegisterTypesTests
    {
        [TestMethod]
        public void Can_register_types_to_the_container()
        {
            IContainer container = new AutoFuncContainer();
            container.Register<IFooRepository, FooRepository>();
            container.Register<ILogger, Logger>();
        }
    }
}