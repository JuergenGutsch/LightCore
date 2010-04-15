using System.Collections.Generic;

using LightCore.Registration;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Fluent.FluentRegistration
{
    [TestFixture]
    public class WhenWithNamedArgumentIsCalled : FluentFixture
    {
        [Test]
        public void WithNull_ArgumentCountStaysOnZero()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.WithNamedArguments(null);

            Assert.That(registrationItem.Arguments.CountOfAllArguments, Is.EqualTo(0));
        }

        [Test]
        public void WithOneNamedFooArgument_TheArgumentIsTheSame()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);
            var key = "test";
            var foo = new Foo();

            fluentRegistration.WithNamedArguments(new Dictionary<string, object> { { key, foo } });

            Assert.That(registrationItem.Arguments.NamedArguments.Count, Is.EqualTo(1));
            Assert.That(registrationItem.Arguments.NamedArguments[key], Is.SameAs(foo));
        }

        [Test]
        public void WithTwoNamedAnonymousTypeArguments_TheArgumentsAreCommited()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.WithNamedArguments(new { Foo = "foo", Bar = new Bar() });

            Assert.That(registrationItem.Arguments.NamedArguments.Count, Is.EqualTo(2));
            Assert.That(registrationItem.Arguments.NamedArguments["Foo"], Is.InstanceOf<string>());
            Assert.That(registrationItem.Arguments.NamedArguments["Bar"], Is.InstanceOf<Bar>());
        }
    }
}