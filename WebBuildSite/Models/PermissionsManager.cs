using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Xml.Linq;
using WebBuildLib.Extensions;

namespace WebBuildSite.Models
{
    public class PermissionsManager
    {
        private BuildPermissions _permissions;
        private string _pathToPermissionsFile
        {
            get
            {
                if (ConfigurationManager.AppSettings["PathToPermissionsFile"] == null)
                {
                    return null;
                }
                return HostingEnvironment.MapPath(ConfigurationManager.AppSettings["PathToPermissionsFile"].ToString());
            }
        }
        public PermissionsManager()
        {
            LoadPermissions();
        }

        public bool CanViewStagingBuilds(string user_name)
        {
            CanViewStaging name =_permissions.CanViewStaging.Where(x => x.Name == user_name.ToUpper()).FirstOrDefault();
            if (name != null) return true;
            return false;
        }
        public bool CanViewQABuilds(string user_name)
        {
            CanViewRCQA name = _permissions.CanViewQA.Where(x => x.Name == user_name.ToUpper()).FirstOrDefault();
            if (name != null) return true;
            return false;
        }
        public bool CanViewCustomBuildPage(string user_name)
        {
            CanViewCustomBuildPage name = _permissions.CanViewCustomBuildPage.Where(x => x.Name == user_name.ToUpper()).FirstOrDefault();
            if (name != null) return true;
            return false;
        }
        public bool CanViewProductionBuildPage(string user_name)
        {
            CanViewProductionBuildPage name = _permissions.CanViewProductionBuildPage.Where(x => x.Name == user_name.ToUpper()).FirstOrDefault();
            if (name != null) return true;
            return false;
        }
        public bool CanViewDemoBuildPage(string user_name)
        {
            CanViewDemoBuildPage name = _permissions.CanViewDemoBuildPage.Where(x => x.Name == user_name.ToUpper()).FirstOrDefault();
            if (name != null) return true;
            return false;
        }
        public bool CanOverrideBuildInProgress(string user_name)
        {
            CanOverrideBuildInProgress name = _permissions.CanOverrideBuildInProgress.Where(x => x.Name == user_name.ToUpper()).FirstOrDefault();
            if (name != null) return true;
            return false;
        }
        private void LoadPermissions()
        {
            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(_pathToPermissionsFile);
            _permissions = xdoc.Deserialize<BuildPermissions>();
        }
    }
}
