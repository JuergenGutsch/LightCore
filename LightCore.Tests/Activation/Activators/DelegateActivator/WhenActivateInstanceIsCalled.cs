using System;
using System.Collections.Generic;
using LightCore.Activation;
using LightCore.Activation.Activators;
using LightCore.Registration;
using LightCore.TestTypes;

using Xunit;
using FluentAssertions;

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
            var delegateActivator = this.GetActivator(activatorFunction);

            object result = delegateActivator.ActivateInstance(new ResolutionContext());

            result.Should().BeOfType<object>();
        }

        //[Fact]
        //public void WithFunctionAndFullBlownResolutionContext_IFooWithDependendenciesReturned()
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