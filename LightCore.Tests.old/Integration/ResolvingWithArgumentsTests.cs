using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class ResolvingWithArgumentsTests
    {
        [Test]
        public void Foo_is_resolved_with_bool_argument_constructor_if_only_that_argument_is_supported()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments(true);

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(true, (instance as Foo).Arg2);
        }

        [Test]
        public void Container_resolves_instances_with_arguments()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();

            builder
                .Register<IFoo, Foo>()
                .WithArguments("Peter", true);

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();

            Assert.AreEqual("Peter", ((Foo)foo).Arg1);
            Assert.AreEqual(true, ((Foo)foo).Arg2);
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