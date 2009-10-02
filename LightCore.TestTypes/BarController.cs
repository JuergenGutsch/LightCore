using System.Web.Mvc;

namespace PeterBucher.AutoFunc.TestTypes
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