using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class ResolvingWithArgumentsTests
    {
        [Test]
        public void Container_resolves_instances_with_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>().WithArguments("Peter");
            builder.Register<IBar, Bar>().WithArguments("Peter", true).WithName("TwoArguments");

            var container = builder.Build();

            var bar = container.Resolve<IBar>();
            var barWithTwoArguments = container.Resolve<IBar>("TwoArguments");

            Assert.AreEqual("Peter", ((Bar)bar).Arg1);
            Assert.AreEqual("Peter", ((Bar)barWithTwoArguments).Arg1);
            Assert.AreEqual(true, ((Bar)barWithTwoArguments).Arg2);
        }

        [Test]
        public void Container_resolves_instances_with_dependencies_and_arguments()
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