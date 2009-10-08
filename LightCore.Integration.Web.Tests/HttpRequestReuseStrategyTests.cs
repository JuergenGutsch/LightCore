using System.Collections.Generic;
using System.Web;

using LightCore.Integration.Web.Scope;
using LightCore.Scope;

using NUnit.Framework;

using Moq;

namespace LightCore.Integration.Web.Tests
{
    [TestFixture]
    public class HttpRequestReuseStrategyTests
    {
        [Test]
        public void Instance_is_reused_in_same_httprequestscope()
        {
            var builder = new ContainerBuilder();

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            var scopeMock = new Mock<ScopeBase>();
            scopeMock
                .Setup(s => 

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>().ScopedTo(() => new HttpRequestScope
                                                             {
                                                                 CurrentContext = currentContext.Object
                                                             });

            var container = builder.Build();

            var firstResolvedInstanceInContext = container.Resolve<IFoo>();
            var secondResolvedInstanceInContext = container.Resolve<IFoo>();

            Assert.AreSame(firstResolvedInstanceInContext, secondResolvedInstanceInContext);
        }

        [Test]
        public void Instance_is_not_reused_in_different_httprequestscopes()
        {
            var builder = new ContainerBuilder();

            var currentItems = new Dictionary<string, object>();

            var currentContext = new Mock<HttpContextBase>();
            currentContext
                .Setup(c => c.Items)
                .Returns(currentItems);

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>().ScopedTo(() => new HttpRequestReuseStrategy
                                                             {
                                                                 CurrentContext = currentContext.Object
                                                             });

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