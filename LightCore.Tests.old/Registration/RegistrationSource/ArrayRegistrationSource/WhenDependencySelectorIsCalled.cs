using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.ArrayRegistrationSource
{
    [TestFixture]
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithArrayType_TheSourceCanHandle()
        {
            var registrationSource = this.GetArrayRegistrationSource(typeof(object));

            Assert.That(registrationSource.SourceSupportsTypeSelector(typeof(object[])), Is.True);
        }

        [Test]
        public void WithAbstractType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetArrayRegistrationSource();

            Assert.That(registrationSource.SourceSupportsTypeSelector(typeof(FooBase)), Is.False);
        }

        [Test]
        public void WithConcreteType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetArrayRegistrationSource();

            Assert.That(registrationSource.SourceSupportsTypeSelector(typeof(Foo)), Is.False);
        }
    }
}