using System;
using FluentAssertions;
using LightCore.Activation;
using LightCore.Activation.Activators;
using Xunit;

namespace LightCore.Tests.Activation.Activators.DelegateActivator
{
    public class WhenActivateInstanceIsCalled
    {
        private IActivator GetActivator(Func<IContainer, object> activatorFunction)
        {
            return new LightCore.Activation.Activators.DelegateActivator(activatorFunction);
        }

        [Fact]
        public void WithFunctionAndEmptyResolutionContext_ObjectReturned()
        {
            Func<IContainer, object> activatorFunction = c => new object();
            var delegateActivator = GetActivator(activatorFunction);

            var result = delegateActivator.ActivateInstance(new ResolutionContext());

            result.Should().BeOfType<object>();
        }

        //public void WithFunctionAndFullBlownResolutionContext_IFooWithDependendenciesReturned()

        //[Fact]
        //{
        //    Func<IContainer, object> activatorFunction = c => new Foo(c.Resolve<IBar>());
        //    var delegateActivator = this.GetActivator(activatorFunction);

        //    containerMock
        //        .Setup(c => c.Resolve<IBar>())
        //        .Returns(new Bar());

        //    var result = (IFoo)delegateActivator.ActivateInstance(
        //        new ResolutionContext(containerMock.Object, new RegistrationContainer()));

        //    result.Should().NotBeNull();
        //    result.Bar.Should().NotBeNull();
        //}
    }
}