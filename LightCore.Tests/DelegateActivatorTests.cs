using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class DelegateActivatorTests
    {
        [Test]
        public void DelegateActivator_can_return_an_instance_from_given_new_function()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo());

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();

            Assert.IsNotNull(foo);
        }

        [Test]
        public void DelegateActivator_can_return_same_object_with_process_scope()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo());

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            Assert.AreSame(foo, foo2);
        }

        [Test]
        public void DelegateActivator_can_return_a_named_instance()
        {
            var builder = new ContainerBuilder();

            builder
                .Register<IFoo>(c => new Foo())
                .WithName("test");

            var container = builder.Build();

            var foo = container
                .Resolve<IFoo>(("test"));

            Assert.IsNotNull(foo);
        }
    }
}