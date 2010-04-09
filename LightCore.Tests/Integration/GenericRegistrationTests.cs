using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class GenericRegistrationTest
    {
        [Test]
        public void Generic_registration_can_registered_with_delegates()
        {
            var builder = new ContainerBuilder();
            builder.Register<IRepository<Foo>, FooRepository>();

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<Foo>>();

            Assert.IsNotNull(fooRepository.GetData());
        }
    }
}