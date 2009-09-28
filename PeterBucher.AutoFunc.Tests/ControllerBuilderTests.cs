using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.Exceptions;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class ControllerBuilderTests
    {
        [TestMethod]
        public void ControllerBuilder_can_register_types()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>();
            builder.Register<ILogger, Logger>();
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationAlreadyExistsException))]
        public void ContainerBuilder_throws_exception_on_duplicate_registration()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>();
            builder.Register<IFooRepository, FooRepository>();

            var container = builder.Build();
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationAlreadyExistsException))]
        public void ContainerBuilder_throws_exception_on_duplicate_name_registration()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>().WithName("foo");
            builder.Register<IFooRepository, BarRepository>().WithName("foo");

            var contianer = builder.Build();
        }
    }
}