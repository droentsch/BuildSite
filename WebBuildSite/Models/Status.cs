using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebBuildSite.Models.Contract
{
    public class Status
    {
        public bool Error { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public string DateTime { get; set; }
    }
}
