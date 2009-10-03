using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightCore.Tests
{
    [TestClass]
    public class DelegateActivatorTests
    {
        [TestMethod]
        public void DelegateActivator_can_return_an_instance_from_given_instance()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo());

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();

            Assert.IsNotNull(foo);
        }

        [TestMethod]
        public void DelegateActivator_can_return_same_object_with_singleton_reuse_scope()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo()).ScopedToSingleton();

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            Assert.AreSame(foo, foo2);
        }

        [TestMethod]
        public void DelegateActivator_can_return_a_named_instance()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo()).WithName("test");

            var container = builder.Build();

            var foo = container.ResolveNamed<IFoo>("test");

            Assert.IsNotNull(foo);
        }
    }
}