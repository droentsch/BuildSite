using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildFoundation;

namespace WebBuildLib
{
    [Serializable]
    public class PlatformManager
    {
        private Platform _platform;
        public Platform Projects
        {
            get
            {
                return _platform;
            }
            set
            {
                _platform = value;
            }
        }

        public PlatformManager()
        {
            _platform = null;
        }
        public PlatformManager(Platform platform)
        {
            _platform = platform;
        }
        public ItemInfo GetVersion(string _projectName)
        {
            if (_platform == null)
            {
                throw new Exception("Platform not defined.");
            }
            if (String.IsNullOrEmpty(_projectName))
            {
                throw new ArgumentNullException("Project name cannot be null.");
            }
            ItemInfo iteminfo = null;
            switch (_projectName)
            {
                case "PublicWeb":
                    iteminfo = _platform.PublicWeb;
                    iteminfo.Name = "PublicWeb";
                    break;
                case "Xigen":
                    iteminfo = _platform.Xigen;
                    iteminfo.Name = "Xigen";
                    break;
                case "CmsService":
                    iteminfo = _platform.CmsService;
                    iteminfo.Name = "CmsService";
                    break;
                case "PublicService":
                    iteminfo = _platform.PublicService;
                    iteminfo.Name = "PublicService";
                    break;
                case "CmsWeb":
                    iteminfo = _platform.CmsWeb;
                    iteminfo.Name = "CmsWeb";
                    break;
                case "MvcWeb":
                    iteminfo = _platform.MvcWeb;
                    iteminfo.Name = "MvcWeb";
                    break;
            }
            return iteminfo;
        }
        public List<ItemInfo> GetAllVersions()
        {
            List<ItemInfo> items = new List<ItemInfo>();
            _platform.PublicWeb.Name = "PublicWeb";
            items.Add(_platform.PublicWeb);
            _platform.Xigen.Name = "Xigen";
            items.Add(_platform.Xigen);
            _platform.CmsService.Name = "CmsService";
            items.Add(_platform.CmsService);
            _platform.PublicService.Name = "PublicService";
            items.Add(_platform.PublicService);
            _platform.CmsWeb.Name = "CmsWeb";
            items.Add(_platform.CmsWeb);
            _platform.MvcWeb.Name = "MvcWeb";
            items.Add(_platform.MvcWeb);
            return items;
        }
    }
}
