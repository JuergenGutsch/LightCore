using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Tests.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class ContainerTests
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Can_initialize_the_container()
        {
            IContainer container = new AutoFuncContainer();

            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void Can_create_an_instance_from_contract()
        {
            IContainer container = new AutoFuncContainer();
            container.Register<IFooRepository, FooRepository>();
            container.Register<ILogger, Logger>();

            IFooRepository fooRepository = container.Resolve<IFooRepository>();

            Assert.IsNotNull(fooRepository);
            Assert.IsTrue(fooRepository.GetFoos().Count() > 0);
        }

        [TestMethod]
        public void Can_handles_default_transient_lifecycle_correct()
        {
            IContainer container = new AutoFuncContainer();
            container.Register<IFooRepository, FooRepository>();
            container.Register<ILogger, Logger>();

            var rep1 = container.Resolve<IFooRepository>();
            var rep2 = container.Resolve<IFooRepository>();

            Assert.IsFalse(ReferenceEquals(rep1, rep2));
        }

        [TestMethod]
        public void Can_take_care_of_lifecycle_singleton()
        {
            IContainer container = new AutoFuncContainer();
            container.Register<IFooRepository, FooRepository>().AsSingleton();
            container.Register<ILogger, Logger>();

            var rep1 = container.Resolve<IFooRepository>();
            var rep2 = container.Resolve<IFooRepository>();

            Assert.IsTrue(ReferenceEquals(rep1, rep2));
        }

        [TestMethod]
        public void ResolveUp_a_object_tree_works()
        {
            IContainer container = new AutoFuncContainer();
            container.Register<IFooRepository, FooRepository>();
            container.Register<IFooService, FooService>();
            container.Register<ILogger, Logger>();

            var instance = container.Resolve<IFooService>();

            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.GetFoos().Count() > 0);
            Assert.IsNotNull(instance.Logger);
        }
    }
}