using System.Globalization;
using System.Web.Mvc;
using LightCore.TestTypes;

namespace LightCore.Web.Mvc3.IntegrationSample.Controllers
{
    public class HomeController : Controller
    {
        private IFoo _foo;

        public HomeController(IFoo foo)
        {
            this._foo = foo;
        }

        public ActionResult Index()
        {
            if(this._foo != null)
            {
                ViewBag.Message = "ASP.NET MVC 3.0 - funktioniert. (In ASP.NET MVC 4.0 wird LightCore gleich angewandt wie in diesem Beispiel.";
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}