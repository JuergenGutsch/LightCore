using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.ConcreteTypeRegistrationSource
{
    [TestFixture]
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithConcreteType_TheSourceCanHandle()
        {
            var registrationSource = this.GetConcreteRegistrationSource();

            Assert.That(registrationSource.DependencySelector(typeof(Foo)), Is.True);
        }

        [Test]
        public void WithAbstractType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetConcreteRegistrationSource();

            Assert.That(registrationSource.DependencySelector(typeof(FooBase)), Is.False);
        }

        [Test]
        public void WithInterfaceType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetConcreteRegistrationSource();

            Assert.That(registrationSource.DependencySelector(typeof(IFoo)), Is.False);
        }
    }
}