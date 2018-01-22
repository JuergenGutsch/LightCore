using FluentAssertions;
using System.Threading;
using Xunit;

namespace LightCore.Tests.Lifecycle.ThreadSingletonLifecycle
{
    public class WhenRetrieveInstanceInLifecycleIsCalled : LifecycleFixture
    {

        [Fact]
        public void WithActivationFunction_DifferentObjectsPerThreadAreReturned()
        {
            var factory = this.GetActivationFactory();

            var threadData = new ThreadData(new LightCore.Lifecycle.ThreadSingletonLifecycle(), factory);
            var thread = new Thread(threadData.ResolveFoo1WithLifecycle);

            var threadDataTwo = new ThreadData(new LightCore.Lifecycle.ThreadSingletonLifecycle(), factory);
            var threadTwo = new Thread(threadDataTwo.ResolveFoo2WithLifecycle);

            thread.Start();
            threadTwo.Start();

            thread.Join();
            threadTwo.Join();

            threadData.FooOne.Should().NotBe(threadDataTwo.FooOne);
        }
    }
}