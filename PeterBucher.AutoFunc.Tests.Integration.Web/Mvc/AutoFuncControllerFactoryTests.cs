using System.Collections.Generic;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using PeterBucher.AutoFunc.Builder;
using PeterBucher.AutoFunc.Integration.Web.Mvc;
using PeterBucher.AutoFunc.Integration.Web.Reuse;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests.Integration.Web.Mvc
{
    [TestClass]
    public class AutoFuncControllerFactoryTests
    {
        [TestMethod]
        public void Registered_controller_can_be_resolved_by_name()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var registrationModule = new AutoFuncControllerRegistrationModule(typeof(FooController).Assembly);

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            var requestStrategy = new HttpRequestReuseStrategy
                                      {
                                          CurrentContext = currentContext.Object
                                      };

            registrationModule.ReuseStrategy = requestStrategy;

            builder.RegisterModule(registrationModule);

            var container = builder.Build();
            var controllerFactory = new AutoFuncControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void Registered_controller_and_dependencies_can_be_resolved_by_name()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var registrationModule = new AutoFuncControllerRegistrationModule(typeof(FooController).Assembly);

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            var requestStrategy = new HttpRequestReuseStrategy
            {
                CurrentContext = currentContext.Object
            };

            registrationModule.ReuseStrategy = requestStrategy;

            builder.RegisterModule(registrationModule);

            var container = builder.Build();
            var controllerFactory = new AutoFuncControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");

            Assert.IsNotNull(((FooController)controller).Foo);
        }
    }
}