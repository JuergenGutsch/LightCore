using System.Web.Mvc;

namespace LightCore.TestTypes
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