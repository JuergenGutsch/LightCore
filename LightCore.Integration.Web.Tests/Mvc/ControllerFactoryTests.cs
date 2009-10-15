using System.Collections.Generic;
using System.Web;

using LightCore.Integration.Web.Lifecycle;
using LightCore.Integration.Web.Mvc;
using LightCore.TestTypes;

using NUnit.Framework;

using Moq;

namespace LightCore.Integration.Web.Tests.Mvc
{
    [TestFixture]
    public class ControllerFactoryTests
    {
        [Test]
        public void Registered_controller_instances_are_reused_on_httpcontextstrategy()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var registrationModule = new ControllerRegistrationModule(typeof(FooController).Assembly);

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            builder.DefaultControlledBy(() => new HttpRequestLifecycle
                                                         {
                                                             CurrentContext = currentContext.Object
                                                         });

            builder.RegisterModule(registrationModule);

            var container = builder.Build();
            var controllerFactory = new ControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");
            var secondController = controllerFactory.CreateController(null, "bar");

            Assert.IsNotNull(controller);
            Assert.IsNotNull(secondController);
        }

        [Test]
        public void Registered_controller_can_be_resolved_by_name()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var registrationModule = new ControllerRegistrationModule(typeof(FooController).Assembly);

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            builder.DefaultControlledBy(() => new HttpRequestLifecycle
                                                         {
                                                             CurrentContext = currentContext.Object
                                                         });

            builder.RegisterModule(registrationModule);

            var container = builder.Build();
            var controllerFactory = new ControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");

            Assert.IsNotNull(controller);
        }

        [Test]
        public void Registered_controller_and_dependencies_can_be_resolved_by_name()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var registrationModule = new ControllerRegistrationModule(typeof(FooController).Assembly);

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            builder.DefaultControlledBy(() => new HttpRequestLifecycle
                                                         {
                                                             CurrentContext = currentContext.Object
                                                         });

            builder.RegisterModule(registrationModule);

            var container = builder.Build();
            var controllerFactory = new ControllerFactory(container);

            var controller = controllerFactory.CreateController(null, "foo");

            Assert.IsNotNull(((FooController)controller).Foo);
        }
    }
}