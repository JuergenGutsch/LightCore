using System.Collections.Generic;
using System.Web;

using LightCore.Integration.Web.Lifecycle;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

using Moq;

namespace LightCore.Integration.Web.Tests
{
    [TestFixture]
    public class HttpRequestLifecycleTests
    {
        [Test]
        public void Instance_is_reused_in_same_httprequest()
        {
            var builder = new ContainerBuilder();

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            builder.DefaultControlledBy(() => new HttpRequestLifecycle
                                                  {
                                                      CurrentContext = currentContext.Object
                                                  });

            builder.Register<IBar, Bar>().ControlledBy<TransientLifecycle>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var firstResolvedInstanceInContext = container.Resolve<IFoo>();
            var secondResolvedInstanceInContext = container.Resolve<IFoo>();

            Assert.AreSame(firstResolvedInstanceInContext, secondResolvedInstanceInContext);
        }

        [Test]
        public void Instance_is_not_reused_in_different_httprequests()
        {
            var builder = new ContainerBuilder();

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            builder.DefaultControlledBy(() => new HttpRequestLifecycle
                                                  {
                                                      CurrentContext = currentContext.Object
                                                  });

            builder.Register<IBar, Bar>().ControlledBy<TransientLifecycle>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var firstResolvedInstanceInContext = container.Resolve<IFoo>();

            currentItems = new Dictionary<string, object>();

            // Simulate new request scope.
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            var secondResolvedInstanceInContext = container.Resolve<IFoo>();

            Assert.AreNotSame(firstResolvedInstanceInContext, secondResolvedInstanceInContext);
        }
    }
}