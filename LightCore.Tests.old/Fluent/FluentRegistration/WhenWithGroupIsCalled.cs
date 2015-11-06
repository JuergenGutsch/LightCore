using LightCore.Registration;

using NUnit.Framework;

namespace LightCore.Tests.Fluent.FluentRegistration
{
    [TestFixture]
    public class WhenWithGroupIsCalled : FluentFixture
    {
        [Test]
        public void WithNull_GroupIsSetToNull()
        {
            var registrationItem = new RegistrationItem(null);
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.WithGroup(null);

            Assert.That(registrationItem.Group, Is.EqualTo(null));
        }

        [Test]
        public void WithString_GroupIsSetToStringValue()
        {
            var registrationItem = new RegistrationItem(null);
            var fluentRegistration = this.GetRegistration(registrationItem);
            var group = "test";

            fluentRegistration.WithGroup(group);

            Assert.That(registrationItem.Group, Is.EqualTo(group));
        }
    }
}