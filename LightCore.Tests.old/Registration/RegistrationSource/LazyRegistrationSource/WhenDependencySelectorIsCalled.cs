using System;
using System.Collections.Generic;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.LazyRegistrationSource
{
    [TestFixture]
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithNoLazyType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetLazyRegistrationSource( typeof( object ) );

            Assert.That( registrationSource.SourceSupportsTypeSelector( typeof( Func<IFoo> ) ), Is.False );
        }

        [Test]
        public void WithLazyType_TheSourceCanHandle()
        {
            var registrationSource = this.GetLazyRegistrationSource( typeof( IFoo ) );

            Assert.That( registrationSource.SourceSupportsTypeSelector( typeof( Lazy<IFoo> ) ), Is.True );
        }

        [Test]
        public void WithWithLazyTypeButNoRegisteredDependency_TheSourceCannotHandle()
        {
            var registrationSource = this.GetLazyRegistrationSource( typeof( string ) );

            Assert.That( registrationSource.SourceSupportsTypeSelector( typeof( Lazy<IFoo> ) ), Is.False );
        }
    }
}