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
        private ConstructorInfo Select(IEnumerable<ConstructorInfo> constructors, ArgumentContainer arguments, Func<Type, bool> dependencySelector)
        {
            var selector = new LightCore.Activation.Components.ConstructorSelector();

            return selector
                .SelectConstructor(dependencySelector, constructors, arguments, new ArgumentContainer());
        }

        [Test]
        public void WithFooAndNoneRuntimeArguments_DefaultConstructorWasUsed()
        {
            var selector = new LightCore.Activation.Components.ConstructorSelector();

            var finalConstructor = selector.SelectConstructor(
                arg => false,
                typeof (Foo).GetConstructors(),
                null,
                null);

            Assert.That(finalConstructor, Is.Not.Null);
            Assert.That(finalConstructor == typeof (Foo).GetConstructor(Type.EmptyTypes));
        }

        [Test]
        public void WithFooAndNoneArguments_DefaultConstructorWasUsed()
        {
            var finalConstructor = this.Select(
                typeof(Foo).GetConstructors(),
                null,
                arg => false);

            Assert.That(finalConstructor, Is.Not.Null);
            Assert.That(finalConstructor == typeof(Foo).GetConstructor(Type.EmptyTypes));
        }

        [Test]
        public void WithFooAndIBarAsArgument_IBarConstructorWasUsed()
        {
            var finalConstructor = this.Select(
                typeof(Foo).GetConstructors(),
                null,
                arg => arg == typeof(IBar));

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
                arg => arg == typeof(IBar));

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
                    },
                arg => false);

            Assert.That(finalConstructor == typeof(Foo).GetConstructor(new[] { typeof(bool) }));
        }
    }
}