using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBuildLib.State;
using WebBuildSite.Models;

namespace WebBuildSite.Filters.Action
{
    public class StatusFilter : ActionFilterAttribute
    {
        private SiteState _state;
        public StatusFilter()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            _state = new SiteState(_pathToStatusFile, _pathToLogFile);
            filterContext.RouteData.Values.Add("state", _state);
            if (filterContext.ActionDescriptor.ActionName.ToUpper() != "STATUS" 
                && filterContext.ActionDescriptor.ActionName.ToUpper() != "DEFAULT"
                && filterContext.ActionDescriptor.ActionName.ToUpper() != "BUILDINPROGRESS")
            {
                _state.LogMessage("{0} accessed by {1}", filterContext.ActionDescriptor.ActionName, filterContext.HttpContext.User.Identity.Name);
            }

            if (filterContext.ActionDescriptor.ActionName.ToUpper() != "OVERRIDE" &&
                filterContext.ActionDescriptor.ActionName.ToUpper() != "STATUS" &&
                filterContext.ActionDescriptor.ActionName.ToUpper() != "BUILDINPROGRESS")
            {
                if (_state.BuildIsRunning)
                {
                    HandleRunningBuild(filterContext);
                }
                else
                {
                    if (filterContext.ActionDescriptor.ActionName.ToUpper() != "DEFAULT")
                    {
                        _state.BuildIsRunning = true;
                        _state.LastBuildInitiator = filterContext.HttpContext.User.Identity.Name;
                    }
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.ActionDescriptor.ActionName.ToUpper() != "OVERRIDE" &&
               filterContext.ActionDescriptor.ActionName.ToUpper() != "STATUS" &&
               filterContext.ActionDescriptor.ActionName.ToUpper() != "BUILDINPROGRESS" &&
               filterContext.ActionDescriptor.ActionName.ToUpper() != "DEFAULT")
            {
                _state.BuildIsRunning = false;
            }
        }
        #region private methods
        private void HandleRunningBuild(ActionExecutingContext filterContext)
        {
            string detail = String.Format("A build is already in progress, initiated by {0}", _state.LastBuildInitiator);
            if (filterContext.ActionDescriptor.ActionName.ToUpper() == "DEFAULT")
            {
                filterContext.RouteData.Values.Add("BuildInProgressMessage", detail);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "PrimeDev" }, { "action", "BuildInProgress" } });
            }
            else
            {
                ResponseStatus status = new ResponseStatus { Error = true, Detail = detail, DateTime = DateTime.Now.ToString() };
                filterContext.Result = new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        #endregion
        #region private properties
        private string _pathToStatusFile
        {
            get
            {
                if (ConfigurationManager.AppSettings["PathToRuntimeStatusFile"] == null)
                {
                    _state.LogMessage("PathToRuntimeStatusFile doesn't exist in config file.");
                }
                return ConfigurationManager.AppSettings["PathToRuntimeStatusFile"].ToString();
            }
        }
        private string _pathToLogFile
        {
            get
            {
                if (ConfigurationManager.AppSettings["PathToLogFile"] == null)
                {
                    _state.LogMessage("PathToLogFile doesn't exist in config file.");
                }
                return ConfigurationManager.AppSettings["PathToLogFile"].ToString();
            }
        }
        #endregion
    }
}