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
    public class RefreshAllVersionsStatus : Status
    {
        public RefreshAllVersionsStatus()
        {
        }
        public IBuildVersion Data { get; set; }
    }
}