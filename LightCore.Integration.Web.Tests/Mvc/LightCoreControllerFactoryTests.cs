using System.Collections.Generic;
using System.Web;

using LightCore.Builder;
using LightCore.Integration.Web.Mvc;
using LightCore.Integration.Web.Reuse;
using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace LightCore.Integration.Web.Tests.Mvc
{
    [TestClass]
    public class LightCoreControllerFactoryTests
    {
        [TestMethod]
        public void Registered_controller_instances_are_reused_on_httpcontextstrategy()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var registrationModule = new LightCoreControllerRegistrationModule(typeof(FooController).Assembly);

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
            var controllerFactory = new LightCoreControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");
            var secondController = controllerFactory.CreateController(null, "bar");

            Assert.IsNotNull(controller);
            Assert.IsNotNull(secondController);
        }

        [TestMethod]
        public void Registered_controller_can_be_resolved_by_name()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var registrationModule = new LightCoreControllerRegistrationModule(typeof(FooController).Assembly);

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
            var controllerFactory = new LightCoreControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void Registered_controller_and_dependencies_can_be_resolved_by_name()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var registrationModule = new LightCoreControllerRegistrationModule(typeof(FooController).Assembly);

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
            var controllerFactory = new LightCoreControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");

            Assert.IsNotNull(((FooController)controller).Foo);
        }
    }
}