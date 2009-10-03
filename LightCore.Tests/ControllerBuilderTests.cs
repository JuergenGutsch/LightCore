using LightCore.Builder;
using LightCore.Exceptions;
using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightCore.Tests
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

        [TestMethod]
        public void ContainerBuilder_can_register_instance()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository>(new FooRepository(new Logger()));

            var container = builder.Build();
        }

        [TestMethod]
        public void ContainerBuilder_can_register_activation_functions()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository>(() => new FooRepository(new Logger()));

            var container = builder.Build();
        }
    }
}