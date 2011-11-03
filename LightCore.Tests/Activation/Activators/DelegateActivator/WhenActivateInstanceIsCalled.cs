using System;

using LightCore.Activation;
using LightCore.Activation.Activators;
using LightCore.Registration;
using LightCore.TestTypes;

using Moq;

using NUnit.Framework;

namespace LightCore.Tests.Activation.Activators.DelegateActivator
{
    [TestFixture]
    public class WhenActivateInstanceIsCalled
    {
        private IActivator GetActivator(Func<IContainer, object> activatorFunction)
        {
            return new LightCore.Activation.Activators.DelegateActivator(activatorFunction);
        }

        [Test]
        public void WithFunctionAndEmptyResolutionContext_ObjectReturned()
        {
            Func<IContainer, object> activatorFunction = c => new object();
            var delegateActivator = this.GetActivator(activatorFunction);

            object result = delegateActivator.ActivateInstance(new ResolutionContext());

            Assert.That(result, Is.InstanceOf<object>());
        }

        [Test]
        public void WithFunctionAndFullBlownResolutionContext_IFooWithDependendenciesReturned()
        {
            Func<IContainer, object> activatorFunction = c => new Foo(c.Resolve<IBar>());
            var delegateActivator = this.GetActivator(activatorFunction);

            var containerMock = new Mock<IContainer>();

            containerMock
                .Setup(c => c.Resolve<IBar>())
                .Returns(new Bar());

            var result = (IFoo)delegateActivator.ActivateInstance(
                new ResolutionContext(containerMock.Object, new RegistrationContainer()));

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Bar, Is.Not.Null);
        }
    }
}