using System.Collections.Generic;
using System.Web;

using LightCore.Builder;
using LightCore.Integration.Web.Reuse;
using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace LightCore.Integration.Web.Tests
{
    [TestClass]
    public class HttpRequestReuseStrategyTests
    {
        [TestMethod]
        public void Instance_is_reused_in_same_httprequestscope()
        {
            var builder = new ContainerBuilder();

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            var requestStrategy = new HttpRequestReuseStrategy
                                      {
                                          CurrentContext = currentContext.Object
                                      };

            builder.Register<IFoo, Foo>().ScopedTo(requestStrategy);

            var container = builder.Build();

            var firstResolvedInstanceInContext = container.Resolve<IFoo>();
            var secondResolvedInstanceInContext = container.Resolve<IFoo>();

            Assert.AreSame(firstResolvedInstanceInContext, secondResolvedInstanceInContext);
        }

        [TestMethod]
        public void Instance_is_not_reused_in_different_httprequestscopes()
        {
            var builder = new ContainerBuilder();

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            var requestStrategy = new HttpRequestReuseStrategy
                                      {
                                          CurrentContext = currentContext.Object
                                      };

            builder.Register<IFoo, Foo>().ScopedTo(requestStrategy);

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