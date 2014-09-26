using BuildFoundation;
using BuildFoundation.Contract;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebBuildLib;
using WebBuildLib.Contract;
using WebBuildLib.State;
using WebBuildSite.Filters.Action;
using WebBuildSite.Models;

namespace WebBuildSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Default()
        {           
            return View();
        }
    }
}
