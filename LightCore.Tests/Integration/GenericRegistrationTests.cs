using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class GenericRegistrationTest
    {
        [Test]
        public void Generic_registration_can_registered_with_closed_type()
        {
            var builder = new ContainerBuilder();
            builder.Register<IRepository<Foo>, FooRepository>();

            var container = builder.Build();

            Assert.That(container.Resolve<IRepository<Foo>>(), Is.Not.Null);
        }

        [Test]
        public void Generic_registration_can_registered_with_open_type()
        {
            var builder = new ContainerBuilder();

            builder.Register(typeof(IRepository<>), typeof(Repository<>));

            var container = builder.Build();

            for (int i = 0; i < 10; i++)
            {
                var fooRepository = container.Resolve<IRepository<Foo>>();
                var barRepository = container.Resolve<IRepository<Bar>>();

                Assert.IsNotNull(fooRepository);
                Assert.IsNotNull(barRepository);

                Assert.IsInstanceOf<Repository<Foo>>(fooRepository);
                Assert.IsInstanceOf<Repository<Bar>>(barRepository);
            }
        }
    }
}