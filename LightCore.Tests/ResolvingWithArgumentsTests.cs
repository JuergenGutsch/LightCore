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
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>().WithArguments("Peter");
            builder.Register<IFoo, Foo>().WithArguments("Peter", true).WithName("TwoArguments");

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();
            var fooWithTwoArguments = container.Resolve<IFoo>("TwoArguments");

            Assert.AreEqual("Peter", ((Foo)foo).Arg1);
            Assert.AreEqual("Peter", ((Foo)fooWithTwoArguments).Arg1);
            Assert.AreEqual(true, ((Foo) fooWithTwoArguments).Arg2);
        }

        [Test]
        public void Container_resolves_instances_with_dependencies_and_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>().WithArguments("Peter", true);

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();

            Assert.IsNotNull(foo.Bar);
            Assert.AreEqual("Peter", ((Foo)foo).Arg1);
            Assert.AreEqual(true, ((Foo)foo).Arg2);
        }
    }
}