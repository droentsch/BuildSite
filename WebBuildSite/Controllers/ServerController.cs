using NapaServerUtilityLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBuildSite.Filters.Action;
using WebBuildSite.Models;

namespace WebBuildSite.Controllers
{
    [Authorize]
    [StatusFilter]
    public class ServerController : Controller
    {
        public string CIRemoteCacheUri
        {
            get
            {
                if (ConfigurationManager.AppSettings["CIRemoteCacheUri"] == null)
                {
                    throw new Exception("CIRemoteCacheUri is not in web.config.");
                }
                return ConfigurationManager.AppSettings["CIRemoteCacheUri"].ToString();
            }
        }
        public string QARemoteCacheUri
        {
            get
            {
                if (ConfigurationManager.AppSettings["QARemoteCacheUri"] == null)
                {
                    throw new Exception("QARemoteCacheUri is not in web.config.");
                }
                return ConfigurationManager.AppSettings["QARemoteCacheUri"].ToString();
            }
        }
        public string CIRemoteIISUri
        {
            get
            {
                if (ConfigurationManager.AppSettings["CIRemoteIISUri"] == null)
                {
                    throw new Exception("CIRemoteIISUri is not in web.config.");
                }
                return ConfigurationManager.AppSettings["CIRemoteIISUri"].ToString();
            }
        }
        public string QARemoteIISUri
        {
            get
            {
                if (ConfigurationManager.AppSettings["QARemoteIISUri"] == null)
                {
                    throw new Exception("QARemoteIISUri is not in web.config.");
                }
                return ConfigurationManager.AppSettings["QARemoteIISUri"].ToString();
            }
        }
        public JsonResult ClearCICache()
        {
            CacheManager mgr = new CacheManager();
            mgr.ClearRemoteCache(CIRemoteCacheUri);
            ResponseStatus status = new ResponseStatus { Error = false, Detail = "CI cache cleared with no errors.", Filepath = "", DateTime = DateTime.Now.ToString(), Links = "" };
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };            
        }
        public JsonResult ClearQACache()
        {
            CacheManager mgr = new CacheManager();
            mgr.ClearRemoteCache(QARemoteCacheUri);
            ResponseStatus status = new ResponseStatus { Error = false, Detail = "QA cache cleared with no errors.", Filepath = "", DateTime = DateTime.Now.ToString(), Links = "" };
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ResetCIIIS()
        {
            IISManager mgr = new IISManager();
            mgr.resetRemoteIIS(CIRemoteIISUri);
            ResponseStatus status = new ResponseStatus { Error = false, Detail = "App Pools Recycled on CI.", Filepath = "", DateTime = DateTime.Now.ToString(), Links = "" };
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ResetQAIIS()
        {
            IISManager mgr = new IISManager();
            mgr.resetRemoteIIS(QARemoteIISUri);
            ResponseStatus status = new ResponseStatus { Error = false, Detail = "App Pools Recycled on QA.", Filepath = "", DateTime = DateTime.Now.ToString(), Links = "" };
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
