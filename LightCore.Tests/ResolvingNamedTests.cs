using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class ResolvingNamedTests
    {
        [Test]
        public void Container_can_resolve_named_registration()
        {
            var builder = new ContainerBuilder();
            builder.Register<ILogger, Logger>();
            builder.Register<ILogger, NullLogger>().WithName("MyNullLogger");

            var container = builder.Build();

            var namedInstance = container.Resolve<ILogger>("MyNullLogger");

            Assert.IsNotNull(namedInstance);
        }
    }
}