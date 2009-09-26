using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.Tests.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class UseDefaultConstructorTests
    {
        [TestMethod]
        public void Default_constructor_is_resolved_if_supposed()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>().UseDefaultConstructor();

            var container = builder.Build();

            var bar = container.Resolve<IBar>();
        }

        [TestMethod]
        public void No_default_constructor_resolved_if_available_but_not_supposed()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            
            var bar = container.Resolve<IBar>();
        }
    }
}