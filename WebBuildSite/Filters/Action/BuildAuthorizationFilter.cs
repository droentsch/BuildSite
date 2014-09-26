using System;
using System.Web.Mvc;
using System.Web.Routing;
using WebBuildLib.State;
using WebBuildSite.Models;

namespace WebBuildSite.Filters.Action
{
    public class BuildAuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            PermissionsManager permissionsMgr = new PermissionsManager();
            if (filterContext.Controller.GetType().Name == "PrimeCustomController")
            {
                if (!permissionsMgr.CanViewCustomBuildPage(filterContext.HttpContext.User.Identity.Name))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Default" } });
                }
            }
            if (filterContext.Controller.GetType().Name == "PrimeDemoController")
            {
                if (!permissionsMgr.CanViewDemoBuildPage(filterContext.HttpContext.User.Identity.Name))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Default" } });
                }
            }
            if (filterContext.Controller.GetType().Name == "PrimeProdController")
            {
                if (!permissionsMgr.CanViewProductionBuildPage(filterContext.HttpContext.User.Identity.Name))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Default" } });
                }
            }
        }
    }
}