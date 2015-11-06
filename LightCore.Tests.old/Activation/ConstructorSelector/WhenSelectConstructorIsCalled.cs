using System;
using System.Collections.Generic;
using System.Reflection;

using LightCore.Registration;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Activation.ConstructorSelector
{
    [TestFixture]
    public class WhenSelectConstructorIsCalled
    {
        private ConstructorInfo Select(IEnumerable<ConstructorInfo> constructors, ArgumentContainer arguments)
        {
            return this.Select(constructors, arguments, new Type[] { });
        }

        private ConstructorInfo Select(IEnumerable<ConstructorInfo> constructors, params Type[] registeredTypes)
        {
            return this.Select(constructors, new ArgumentContainer(), registeredTypes);
        }

        private ConstructorInfo Select(IEnumerable<ConstructorInfo> constructors, ArgumentContainer arguments, params Type[] registeredTypes)
        {
            var selector = new LightCore.Activation.Components.ConstructorSelector();

            var resolutionContext =
                new LightCore.Activation.ResolutionContext(
                    null,
                    RegistrationHelper.GetRegistrationContainerFor(registeredTypes),
                    arguments,
                    new ArgumentContainer());

            return selector
                .SelectConstructor(constructors, resolutionContext);
        }

        [Test]
        public void WithFooAndNoneRuntimeArguments_DefaultConstructorWasUsed()
        {
            var selector = new LightCore.Activation.Components.ConstructorSelector();

            var finalConstructor = this.Select(
                typeof(Foo).GetConstructors());

            Assert.That(finalConstructor, Is.Not.Null);
            Assert.That(finalConstructor == typeof(Foo).GetConstructor(Type.EmptyTypes));
        }

        [Test]
        public void WithFooAndNoneArguments_DefaultConstructorWasUsed()
        {
            var finalConstructor = this.Select(
                typeof(Foo).GetConstructors());

            Assert.That(finalConstructor, Is.Not.Null);
            Assert.That(finalConstructor == typeof(Foo).GetConstructor(Type.EmptyTypes));
        }

        [Test]
        public void WithFooAndIBarAsArgument_IBarConstructorWasUsed()
        {
            var finalConstructor = this.Select(
                typeof(Foo).GetConstructors(),
                typeof(IBar));

            Assert.That(finalConstructor == typeof(Foo).GetConstructor(new[] { typeof(IBar) }));
        }

        [Test]
        public void WithFooAndIBarAndAStringAsArguments_IBarAndStringConstructorWasUsed()
        {
            var finalConstructor = this.Select(
                typeof(Foo).GetConstructors(),
                new ArgumentContainer
                    {
                        AnonymousArguments = new[] { "Peter" }
                    },
               typeof(IBar));

            Assert.That(finalConstructor == typeof(Foo).GetConstructor(new[] { typeof(IBar), typeof(string) }));
        }

        [Test]
        public void WithFooAndABoolAsArgument_BoolConstructorWasUsed()
        {
            var finalConstructor = this.Select(
                typeof(Foo).GetConstructors(),
                new ArgumentContainer
                    {
                        AnonymousArguments = new object[] { true }
                    });

            Assert.That(finalConstructor == typeof(Foo).GetConstructor(new[] { typeof(bool) }));
        }
    }
}