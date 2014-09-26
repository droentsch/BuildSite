using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBuildSite.Models.Contract;
using WebBuildLib.Contract;
using WebBuildLib;
using BuildFoundation.Contract;

namespace WebBuildSite.Models
{
    public class RefreshStatus : Status
    {
        public RefreshStatus()
        {
        }
        public IItemInfo Data { get; set; }
    }
}