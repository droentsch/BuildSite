using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBuildSite.Models.Contract;

namespace WebBuildSite.Models
{
    public class ResponseStatus : Status
    {
        public static string SINGLE = "Single";
        public static string MULTI = "Multi";
        public ResponseStatus()
        {
        }
        public string Links { get; set; }
        public string Filepath { get; set; }
    }
}