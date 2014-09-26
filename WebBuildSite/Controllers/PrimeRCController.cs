using BuildFoundation;
using BuildFoundation.Contract;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using WebBuildLib;
using WebBuildLib.Contract;
using WebBuildLib.State;
using WebBuildSite.Filters.Action;
using WebBuildSite.Models;
using WebBuildSite.Models.Contract;

namespace WebBuildSite.Controllers
{
    [StatusFilter]
    public class PrimeRCController : Controller
    {
        PermissionsManager permissionsMgr;
        public ActionResult Default()
        {
            ViewBag.IsAuthorizedToViewStagingBuilds = false;
            ViewBag.IsAuthorizedToViewCustomBuilds = false;
            ViewBag.IsAuthorizedToViewDemoBuilds = false;

            if (permissionsMgr.CanViewStagingBuilds(HttpContext.User.Identity.Name))
            {
                ViewBag.IsAuthorizedToViewStagingBuilds = true;
            }
            if (permissionsMgr.CanViewCustomBuildPage(HttpContext.User.Identity.Name))
            {
                ViewBag.IsAuthorizedToViewCustomBuilds = true;
            }
            if (permissionsMgr.CanViewQABuilds(HttpContext.User.Identity.Name))
            {
                ViewBag.IsAuthorizedToViewRCQABuilds = true;
            }
            if (permissionsMgr.CanViewDemoBuildPage(HttpContext.User.Identity.Name))
            {
                ViewBag.IsAuthorizedToViewDemoBuilds = true;
            }
            if (permissionsMgr.CanViewProductionBuildPage(HttpContext.User.Identity.Name))
            {
                ViewBag.IsAuthorizedToViewProductionBuilds = true;
            }
            return View("Default");
        }
        #region CI
        [HttpPost]
        public JsonResult BuildCIMVCSite(string recipients)
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            if (!String.IsNullOrEmpty(recipients))
            {
                string decodedRecips = HttpUtility.UrlDecode(recipients);
               _buildManager.AddRecipients(decodedRecips);
            }
            builds.Add(_buildManager.RunBuild("RC", "CI", "PrimeMVC"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildCIWFSite()
        {
            IProjectBuild b = _buildManager.RunBuild("RC", "CI", "PrimeWF");
            return SuccessResponse(b);
        }
        [HttpPost]
        public JsonResult BuildCIDBProject()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "CI", "PrimeDB"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildCIApiSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "CI", "PrimeAPI"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildCILINKSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "CI", "PrimeLINK"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildCIFileTransferProject()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "CI", "FileTransferService"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildCIAll()
        {
            List<IProjectBuild> builds = _buildManager.RunBuilds("RC", "CI");
            return SuccessResponse(builds);
        }
        #endregion
        #region QA
        [HttpPost]
        public JsonResult BuildQAMVCSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "QA", "PrimeMVC"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildQAWFSite()
        {
            IProjectBuild b = _buildManager.RunBuild("RC", "QA", "PrimeWF");
            return SuccessResponse(b);
        }
        [HttpPost]
        public JsonResult BuildQADBProject()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "QA", "PrimeDB"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildQAApiSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "QA", "PrimeAPI"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildQALINKSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "QA", "PrimeLINK"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildQAFileTransferProject()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "QA", "FileTransferService"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildQAAll()
        {
            List<IProjectBuild> builds = _buildManager.RunBuilds("RC", "QA");
            return SuccessResponse(builds);
        }
        #endregion
        #region STAGING
        [HttpPost]
        public JsonResult BuildStagingMVCSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "Staging", "PrimeMVC"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildStagingWFSite()
        {
            IProjectBuild b = _buildManager.RunBuild("RC", "Staging", "PrimeWF");
            return SuccessResponse(b);
        }
        [HttpPost]
        public JsonResult BuildStagingDBProject()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "Staging", "PrimeDB"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildStagingApiSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "Staging", "PrimeAPI"));
            return SuccessResponse(builds);
        }
        public JsonResult BuildStagingLINKSite()
        {
            List<IProjectBuild> builds = new List<IProjectBuild>();
            builds.Add(_buildManager.RunBuild("RC", "Staging", "PrimeLINK"));
            return SuccessResponse(builds);
        }
        [HttpPost]
        public JsonResult BuildStagingAll()
        {
            List<IProjectBuild> builds = _buildManager.RunBuilds("RC", "Staging");
            return SuccessResponse(builds);
        }

        #endregion
        #region application status
        public ActionResult Status()
        {
            SiteState state = (SiteState)RouteData.Values["state"];
            if (state != null)
            {
                return View(state.Logs); 
            }
            return View();
        }
        public ActionResult Override()
        {
            SiteState state = (SiteState)RouteData.Values["state"];
            state.BuildIsRunning = false;

            return View("Default");
        }
        public ActionResult BuildInProgress()
        {
            ViewBag.Message = "There is a build in progress";
            if (RouteData.Values["BuildInProgressMessage"] != null)
            {
                ViewBag.Message = RouteData.Values["BuildInProgressMessage"].ToString();
            }
            return View();
        }
        #endregion
        #region private members
        private BuildConfigManager _bcm;
        private BuildConfigManager _buildConfigManager
        {
            get
            {
                try
                {
                    _bcm = (BuildConfigManager)this.HttpContext.Application["BUILD_CONFIG"];
                    if (_bcm == null && _bcm.BuildConfigs.Count == 0)
                    {
                        throw new Exception(String.Format("No build configurations have been loaded."));
                    }
                    return _bcm;
                }
                catch (Exception ex)
                {
                    throw new Exception("Problem accessing BuildConfigManager.", ex.InnerException);
                }
            }
        }
        private ProjectBuildManager _bm;
        private ProjectBuildManager _buildManager
        {
            get
            {
                if (_bm == null)
                {
                    _bm = new ProjectBuildManager(_buildConfigManager);
                }
                return _bm;
            }
        }
        
        #endregion
        #region private methods
        private JsonResult SuccessResponse(IProjectBuild build)
        {
            ResponseStatus status = new ResponseStatus { Error = false, Detail = String.Format("Project {0} built with no errors. ({1}) ", build.ShortName, build.Configuration.Description), Filepath = "", DateTime = DateTime.Now.ToString(), Links = build.Link };
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private JsonResult SuccessResponse(List<IProjectBuild> builds)
        {
            ResponseStatus status = new ResponseStatus();
            status.Error = false;
            status.DateTime = DateTime.Now.ToString();
            status.Detail = "Projects built with no errors:\n\n";
            foreach (var build in builds)
            {
                status.Detail += String.Format("{0} ({1})\n", build.ShortName, build.Configuration.Description);
                status.Links += String.Format("{0}\n", build.Link);
            }
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private JsonResult RefreshResponse(ItemInfo iteminfo, string _platform)
        {
            RefreshStatus status = new RefreshStatus { Error = false, DateTime = DateTime.Now.ToString(), Data = iteminfo, Detail = _platform, Type = ResponseStatus.SINGLE };
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private JsonResult RefreshResponse(List<ItemInfo> iteminfos, string _platform)
        {
            List<RefreshStatus> statuses = new List<RefreshStatus>();
            foreach (var iteminfo in iteminfos)
            {
                statuses.Add(new RefreshStatus { Error = false, DateTime = DateTime.Now.ToString(), Data = (IItemInfo)iteminfo, Detail = _platform, Type = ResponseStatus.MULTI });
            }
            return new JsonResult { Data = statuses, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private JsonResult RefreshAllVersionsResponse(BuildVersion versions, string state)
        {
            RefreshAllVersionsStatus status = new RefreshAllVersionsStatus { Error = false, DateTime = DateTime.Now.ToString(), Data = versions, Detail = string.Format("{0} version set", state), Type = ResponseStatus.MULTI };
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private JsonResult StatusResponse(bool is_success)
        {
            WebBuildSite.Models.Contract.Status status = new WebBuildSite.Models.Contract.Status();
            status.Error = !is_success;
            status.DateTime = DateTime.Now.ToString();
            status.Detail = "Type set";
            if (!is_success)
            {
                status.Detail = "Type not set";
            }
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion
        #region constructor
        public PrimeRCController()
        {
            permissionsMgr = new PermissionsManager();
        }
        #endregion
    }
}
