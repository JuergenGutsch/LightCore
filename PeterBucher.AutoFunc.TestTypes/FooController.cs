using System.Web.Mvc;

namespace PeterBucher.AutoFunc.TestTypes
{
    public class FooController : Controller
    {
        public IFoo Foo
        {
            get;
            set;
        }

        public FooController()
        {
            
        }

        public FooController(IFoo foo)
        {
            this.Foo = foo;
        }
    }
}