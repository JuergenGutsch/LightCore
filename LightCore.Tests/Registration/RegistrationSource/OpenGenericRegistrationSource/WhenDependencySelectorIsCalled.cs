using System.Collections.Generic;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.OpenGenericRegistrationSource
{
    [TestFixture]
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithNull_TheSourceCannotHandle()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(object), typeof(object));

            Assert.That(registrationSource.DependencySelector(typeof(object)), Is.False);
        }

        [Test]
        public void WithNoOpenGenericType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(object), typeof(object));

            Assert.That(registrationSource.DependencySelector(typeof(List<object>)), Is.False);
        }

        [Test]
        public void WithOpenGenericTypeRegistered_TheSourceCanHandle()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IList<>), typeof(List<>));

            Assert.That(registrationSource.DependencySelector(typeof(IList<object>)), Is.True);
        }

        [Test]
        public void WithOpenGenericTypeAndTwoTypeArguments_TheSourceCanHandle()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IRepository<,>), typeof(Repository<>));

            Assert.That(registrationSource.DependencySelector(typeof(IRepository<Foo, int>)), Is.True);
        }
    }
}