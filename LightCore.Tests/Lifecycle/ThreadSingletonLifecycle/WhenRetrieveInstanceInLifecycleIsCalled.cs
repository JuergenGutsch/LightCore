using System.Threading;

using NUnit.Framework;

namespace LightCore.Tests.Lifecycle.ThreadSingletonLifecycle
{
    [TestFixture]
    public class WhenRetrieveInstanceInLifecycleIsCalled : LifecycleFixture
    {
        [Test]
        public void WithActivationFunction_DifferentObjectsPerThreadAreReturned()
        {
            var lifecycle = new LightCore.Lifecycle.ThreadSingletonLifecycle();
            var factory = this.GetActivationFactory();

            var threadData = new ThreadData(lifecycle, factory);
            var thread = new Thread(threadData.ResolveFoosWithLifecycle);

            var threadDataTwo = new ThreadData(lifecycle, factory);
            var threadTwo = new Thread(threadDataTwo.ResolveFoosWithLifecycle);

            thread.Start();
            threadTwo.Start();

            thread.Join();
            threadTwo.Join();

            Assert.IsTrue(ReferenceEquals(threadData.FooOne, threadData.FooTwo));
            Assert.IsFalse(ReferenceEquals(threadData.FooOne, threadDataTwo.FooOne));
        }
    }
}