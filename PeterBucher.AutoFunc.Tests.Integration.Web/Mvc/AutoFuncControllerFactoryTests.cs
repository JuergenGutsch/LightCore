using System.Web.Mvc;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.Integration.Web.Mvc;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests.Integration.Web.Mvc
{
    [TestClass]
    public class AutoFuncControllerFactoryTests
    {
        [TestMethod]
        public void Can_resolve_registered_controller()
        {
            var builder = new ContainerBuilder();

            builder.Register<IController, FooController>()
                .UseDefaultConstructor()
                .WithName("one");

            var container = builder.Build();
            var controllerFactory = new AutoFuncControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "one");

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void Can_Resolve_registered_controller_with_dependencies()
        {
            var builder = new ContainerBuilder();

            builder.Register<IController, FooController>()
                .WithName("one");

            builder.Register<IFoo, Foo>();

            var container = builder.Build();
            var controllerFactory = new AutoFuncControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "one");

            Assert.IsNotNull(((FooController) controller).Foo);
        }
    }
}