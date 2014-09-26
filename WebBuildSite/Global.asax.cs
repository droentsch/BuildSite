using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Web.Caching;
using System.Web.Hosting;
using WebBuildLib;
using WebBuildSite.Filters.Action;

namespace WebBuildSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ErrorFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "PrimeDev", action = "Default", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BuildConfigManager bcm = GetBuildConfig();
            Application.Add("BUILD_CONFIG", bcm);
        }
        #region private
        private BuildConfigManager GetBuildConfig()
        {
            if (ConfigurationManager.AppSettings["BuildConfigPath"] == null)
            {
                throw new ConfigurationErrorsException("BuildConfigPath not found in web.config");
            }
            string _buildConfigPath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["BuildConfigPath"].ToString());

            BuildConfigManager currentBuild = new BuildConfigManager(_buildConfigPath);

            return currentBuild;
        }
        #endregion
    }
}