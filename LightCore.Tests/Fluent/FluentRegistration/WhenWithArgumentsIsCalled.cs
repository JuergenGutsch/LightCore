using LightCore.Registration;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Fluent.FluentRegistration
{
    [TestFixture]
    public class WhenWithArgumentIsCalled : FluentFixture
    {
        [Test]
        public void WithNull_ArgumentCountStaysOnZero()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.WithArguments(null);

            Assert.That(registrationItem.Arguments.CountOfAllArguments, Is.EqualTo(0));
        }

        [Test]
        public void WithTwoArguments_AnonymousArgumentCountIsTwo()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.WithArguments(2, 3);

            Assert.That(registrationItem.Arguments.AnonymousArguments.Length, Is.EqualTo(2));
        }

        [Test]
        public void WithOneFooArgument_TheArgumentIsTheSame()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);
            var foo = new Foo();

            fluentRegistration.WithArguments(foo);

            Assert.That(registrationItem.Arguments.AnonymousArguments.Length, Is.EqualTo(1));
            Assert.That(registrationItem.Arguments.AnonymousArguments[0], Is.SameAs(foo));
        }
    }
}