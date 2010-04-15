using System.Collections.Generic;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.EnumerableRegistrationSource
{
    [TestFixture]
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithEnumerableType_TheSourceCanHandle()
        {
            var registrationSource = this.GetEnumerableRegistrationSource(typeof(object));

            Assert.That(registrationSource.DependencySelector(typeof(IEnumerable<object>)), Is.True);
        }

        [Test]
        public void WithAbstractType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetEnumerableRegistrationSource();

            Assert.That(registrationSource.DependencySelector(typeof(FooBase)), Is.False);
        }

        [Test]
        public void WithConcreteType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetEnumerableRegistrationSource();

            Assert.That(registrationSource.DependencySelector(typeof(Foo)), Is.False);
        }
    }
}