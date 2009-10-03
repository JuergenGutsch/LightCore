using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightCore.Tests
{
    [TestClass]
    public class ResolvingUseDefaultConstructorTests
    {
        [TestMethod]
        public void Container_resolving_with_default_constructor_as_supposed()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>().UseDefaultConstructor();

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            Assert.IsNull(((Bar)bar).Foo);
        }

        [TestMethod]
        public void Container_resolving_with_non_default_constructor_as_supposed()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            Assert.IsNotNull(((Bar)bar).Foo);
        }
    }
}