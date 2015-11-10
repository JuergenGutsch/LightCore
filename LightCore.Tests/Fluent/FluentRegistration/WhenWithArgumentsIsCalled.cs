using FluentAssertions;
using LightCore.Registration;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Fluent.FluentRegistration
{
    public class WhenWithArgumentIsCalled : FluentFixture
    {
        [Fact]
        public void WithNull_ArgumentCountStaysOnZero()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = GetRegistration(registrationItem);

            fluentRegistration.WithArguments(null);

            registrationItem.Arguments.CountOfAllArguments.Should().Be(0);
        }

        [Fact]
        public void WithTwoArguments_AnonymousArgumentCountIsTwo()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = GetRegistration(registrationItem);

            fluentRegistration.WithArguments(2, 3);

            registrationItem.Arguments.AnonymousArguments.Length.Should().Be(2);
        }

        [Fact]
        public void WithOneFooArgument_TheArgumentIsTheSame()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = GetRegistration(registrationItem);
            var foo = new Foo();

            fluentRegistration.WithArguments(foo);

            registrationItem.Arguments.AnonymousArguments.Length.Should().Be(1);
            registrationItem.Arguments.AnonymousArguments[0].Should().BeSameAs(foo);
        }
    }
}