using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class GenericRegistrationTest
    {
        [Test]
        public void Generic_registration_can_registered_with_delegates()
        {
            var builder = new ContainerBuilder();
            builder.Register<IRepository<Foo>, FooRepository>();
            builder.Register<IRepository<Foo>, FooTwoRepository>().WithName("two");

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<Foo>>();
            var fooTwoRepository = container.Resolve<IRepository<Foo>>("two");

            Assert.IsNotNull(fooRepository.GetData());
            Assert.IsNotNull(fooTwoRepository.GetData());
        }
    }
}