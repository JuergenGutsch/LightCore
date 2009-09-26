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
            var builder = new ContainerBuilder();

            builder.Register<IFooRepository, FooRepository>();
            builder.Register<ILogger, Logger>();
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationAlreadyExistsException))]
        public void Cannot_register_types_twice()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>();
            builder.Register<IFooRepository, FooRepository>();

            var container = builder.Build();
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationAlreadyExistsException))]
        public void Cannot_register_twice_with_same_name()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFooRepository, FooRepository>().WithName("foo");
            builder.Register<IFooRepository, BarRepository>().WithName("foo");

            var contianer = builder.Build();
        }
    }
}