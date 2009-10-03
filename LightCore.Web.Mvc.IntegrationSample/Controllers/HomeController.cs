﻿using System.Web.Mvc;
using System.Linq;

using LightCore.Web.Mvc.IntegrationSample.Models;

namespace LightCore.Web.Mvc.IntegrationSample.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IWelcomeRepository _welcomeRepository;

        public HomeController(IWelcomeRepository welcomeRepository)
        {
            this._welcomeRepository = welcomeRepository;
        }

        public ActionResult Index()
        {
            ViewData["Message"] = this._welcomeRepository.GetWelcomeText()
                .Aggregate((current, next) => current + " " + next);

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}