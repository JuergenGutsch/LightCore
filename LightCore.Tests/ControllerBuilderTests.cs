using LightCore.Exceptions;
using LightCore.Reuse;
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
            builder.Register<IFooRepository, FooRepository>();
            builder.Register<ILogger, Logger>();
        }

        [Test]
        public void ContainerBuilder_throws_exception_on_duplicate_registration()
        {
            Assert.Throws<RegistrationAlreadyExistsException>(() =>
                                                                  {
                                                                      var builder = new ContainerBuilder();
                                                                      builder.Register<IFooRepository, FooRepository>();
                                                                      builder.Register<IFooRepository, FooRepository>();

                                                                      var container = builder.Build();
                                                                  });
        }

        [Test]
        public void ContainerBuilder_throws_exception_on_duplicate_name_registration()
        {
            Assert.Throws<RegistrationAlreadyExistsException>(() =>
                                                                  {
                                                                      var builder = new ContainerBuilder();
                                                                      builder.Register<IFooRepository, FooRepository>().WithName("foo");
                                                                      builder.Register<IFooRepository, BarRepository>().WithName("foo");

                                                                      var contianer = builder.Build();
                                                                  });
        }

        [Test]
        public void ContainerBuilder_can_register_instance()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository>(new FooRepository(new Logger()));

            var container = builder.Build();
        }

        [Test]
        public void ContainerBuilder_can_register_activation_functions()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository>(c => new FooRepository(new Logger()));

            var container = builder.Build();
        }

        [Test]
        public void ContainerBuilders_default_scope_is_singleton()
        {
            var builder = new ContainerBuilder();
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

            builder.DefaultScopedTo<TransientReuseStrategy>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instanceOne = container.Resolve<IFoo>();
            var instanceTwo = container.Resolve<IFoo>();

            Assert.AreNotSame(instanceOne, instanceTwo);
        }
    }
}