using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class ControllerBuilderTests
    {
        [Test]
        public void ControllerBuilder_can_register_types()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
        }

        [Test]
        public void ContainerBuilder_throws_exception_on_duplicate_registration()
        {
            Assert.Throws<RegistrationAlreadyExistsException>(() =>
                                                                  {
                                                                      var builder = new ContainerBuilder();
                                                                      builder.Register<IFoo, Foo>();
                                                                      builder.Register<IFoo, Foo>();

                                                                      var container = builder.Build();
                                                                  });
        }

        [Test]
        public void ContainerBuilder_throws_exception_on_duplicate_name_registration()
        {
            Assert.Throws<RegistrationAlreadyExistsException>(() =>
                                                                  {
                                                                      var builder = new ContainerBuilder();
                                                                      builder.Register<IFoo, Foo>().WithName("foo");
                                                                      builder.Register<IFoo, Foo>().WithName("foo");

                                                                      var container = builder.Build();
                                                                  });
        }

        [Test]
        public void ContainerBuilder_throws_on_not_assignable_contract_to_implementation()
        {
            Assert.Throws<ImplementationTypeDoesNotImplemenentContractException>(() =>
            {
                var builder = new ContainerBuilder();
                builder.Register(typeof (IFoo), typeof (Bar));

                var container = builder.Build();
            });
        }

        [Test]
        public void ContainerBuilder_can_register_activation_functions()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo>(c => new Foo(new Bar()));

            var container = builder.Build();
        }
        
        [Test]
        public void ContainerBuilders_default_scope_is_singleton()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instanceOne = container.Resolve<IFoo>();
            var instanceTwo = container.Resolve<IFoo>();

            Assert.AreSame(instanceOne, instanceTwo);
        }

        [Test]
        public void CointainerBuilder_default_scope_can_be_altered_to_transient()
        {
            var builder = new ContainerBuilder();

            builder.DefaultControlledBy<TransientLifecycle>();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instanceOne = container.Resolve<IFoo>();
            var instanceTwo = container.Resolve<IFoo>();

            Assert.AreNotSame(instanceOne, instanceTwo);
        }
    }
}