using System;
using System.Web.Mvc;
using WebBuildLib.State;
using WebBuildSite.Models;

namespace WebBuildSite.Filters.Action
{
    public class ErrorFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            string detail = filterContext.Exception.InnerException == null ? "No detail" : filterContext.Exception.InnerException.ToString();
            ResponseStatus status = new ResponseStatus { Error = true, Detail = filterContext.Exception.Message, DateTime = DateTime.Now.ToString() };
            filterContext.Result = new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            SiteState state = (SiteState)filterContext.RouteData.Values["state"];
            state.BuildIsRunning = false;
        }
    }
}