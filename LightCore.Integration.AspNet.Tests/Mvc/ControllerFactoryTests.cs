using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using LightCore.Integration.Web.Lifecycle;
using LightCore.Integration.Web.Mvc;
using LightCore.TestTypes;

using FluentAssertions;
using Xunit;

using Moq;

namespace LightCore.Integration.Web.Tests.Mvc
{
    public class ControllerFactoryTests
    {
        private ControllerFactory GetControllerFactory()
        {
            var builder = new ContainerBuilder();

            return new ControllerFactory(builder.Build());
        }

        private ControllerFactory GetControllerFactory(IContainer container)
        {
            return new ControllerFactory(container);
        }

        private RequestContext CreateRequestContext()
        {
            var httpRequest = new Mock<HttpRequestBase>();
            var httpContext = new Mock<HttpContextBase>();

            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            return new RequestContext(httpContext.Object, new RouteData());
        }

        [Fact]
        public void ControllerFactory_Throws_ArgumentException_On_Null_Container()
        {
            Action a = () => GetControllerFactory(null);
            a.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ControllerFactory_Throws_ArgumentException_On_Null_Context()
        {
            var controllerFactory = GetControllerFactory();

            Action a = () => controllerFactory.CreateControllerInternal(null, null);
            a.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ControllerFactory_Throws_HttpException_On_Null_ControllerType()
        {
            var controllerFactory = this.GetControllerFactory();

            Action a = () => controllerFactory.CreateControllerInternal(CreateRequestContext(), null);
            a.ShouldThrow<HttpException>();
        }

        [Fact]
        public void Registered_controller_can_be_resolved()
        {
            var builder = new ContainerBuilder();
            builder.Register<IController, FooController>();

            var container = builder.Build();

            var factory = this.GetControllerFactory(container);

            var controller = factory.CreateControllerInternal(this.CreateRequestContext(), typeof(FooController));

            controller.Should().BeOfType<FooController>();
        }

        [Fact]
        public void Registered_controller_and_dependencies_can_be_resolved()
        {
            var builder = new ContainerBuilder();
            builder.Register<IController, FooController>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var factory = this.GetControllerFactory(container);

            var controller = factory.CreateControllerInternal(this.CreateRequestContext(), typeof(FooController));

            controller.Should().NotBeNull();
        }

        [Fact]
        public void Registered_controller_instances_are_reused_on_httpcontextstrategy()
        {
            var builder = new ContainerBuilder();

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

            var controller = controllerFactory.CreateControllerInternal(this.CreateRequestContext(), typeof(FooController));
            var secondController = controllerFactory.CreateControllerInternal(this.CreateRequestContext(), typeof(FooController));
            
            controller.Should().BeSameAs(secondController);
        }
    }
}