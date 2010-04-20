using System.Collections.Generic;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class ResolvingWithRuntimeArgumentsTests
    {
        [Test]
        public void Container_Can_Resolve_With_Given_Runtime_Argument_Successfully()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            IContainer container = builder.Build();

            var foo = (Foo)container.Resolve<IFoo>("yeah");

            Assert.IsNotNull(foo);
            Assert.IsNotNull(foo.Bar);
            Assert.IsNotNull(foo.Arg1);
        }

        [Test]
        public void Container_Does_Not_Reuse_Runtime_Arguments()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var foo = container.Resolve<IFoo>("first");

            var fooBlubs = container.Resolve<IFoo>("yeah", true);

            Assert.AreEqual("yeah", (fooBlubs as Foo).Arg1);
            Assert.IsTrue((fooBlubs as Foo).Arg2);
        }

        [Test]
        public void Container_Can_Resolve_With_Named_Given_Runtime_Argument_Successfully()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            IContainer container = builder.Build();

            var foo = container.Resolve<IFoo>(new Dictionary<string, object> { { "arg2", true }, { "arg1", "Peter" } });

            Assert.IsNotNull(foo);
            Assert.IsTrue(((Foo)foo).Arg2);
            Assert.AreEqual("Peter", ((Foo)foo).Arg1);
        }

        [Test]
        public void Container_Can_Resolve_With_Anonymous_Named_Given_Runtime_Argument_Successfully()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            IContainer container = builder.Build();

            var foo = container.Resolve<IFoo>(new AnonymousArgument(new { arg2 = true, arg1 = "Peter" }));

            Assert.IsNotNull(foo);
            Assert.IsTrue(((Foo)foo).Arg2);
            Assert.AreEqual("Peter", ((Foo)foo).Arg1);
        }
    }
}