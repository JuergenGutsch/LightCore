using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.Tests.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class WithArgumentsTests
    {
        [TestMethod]
        public void Constructor_arguments_are_passed_on_resolve()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>().WithArguments("Peter");
            builder.Register<IBar, Bar>().WithArguments("Peter", true).WithName("TwoArguments");

            var container = builder.Build();

            var bar = container.Resolve<IBar>();
            var barWithTwoArguments = container.ResolveNamed<IBar>("TwoArguments");

            Assert.AreEqual("Peter", ((Bar)bar).Arg1);
            Assert.AreEqual("Peter", ((Bar)barWithTwoArguments).Arg1);
            Assert.AreEqual(true, ((Bar)barWithTwoArguments).Arg2);
        }

        [TestMethod]
        public void Mixed_inner_dependency_properties_and_arguments_works()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>().WithArguments("Peter", true);

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            Assert.IsNotNull(((Bar)bar).Foo);
            Assert.AreEqual("Peter", ((Bar)bar).Arg1);
            Assert.AreEqual(true, ((Bar)bar).Arg2);
        }
    }
}