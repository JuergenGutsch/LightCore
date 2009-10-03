using System.Web.Mvc;

namespace LightCore.TestTypes
{
    public class BarController : Controller
    {
        public IFoo Foo
        {
            get;
            set;
        }

        public BarController()
        {

        }

        public BarController(IFoo foo)
        {
            this.Foo = foo;
        }
    }
}