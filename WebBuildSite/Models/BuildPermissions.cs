using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WebBuildSite.Models
{
    [XmlRoot]
    public class BuildPermissions
    {
        [XmlElement]
        public List<CanViewStaging> CanViewStaging;
        [XmlElement]
        public List<CanViewRCQA> CanViewQA;
        [XmlElement]
        public List<CanViewCustomBuildPage> CanViewCustomBuildPage;
        [XmlElement]
        public List<CanViewDemoBuildPage> CanViewDemoBuildPage;
        [XmlElement]
        public List<CanViewProductionBuildPage> CanViewProductionBuildPage;
        [XmlElement]
        public List<CanOverrideBuildInProgress> CanOverrideBuildInProgress;
    }

    public class CanViewStaging
    {
        public string Name;
    }
    public class CanViewCustomBuildPage
    {
        public string Name;
    }
    public class CanViewDemoBuildPage
    {
        public string Name;
    }
    public class CanViewProductionBuildPage
    {
        public string Name;
    }
    public class CanViewRCQA
    {
        public string Name;
    }
    public class CanOverrideBuildInProgress
    {
        public string Name;
    }

}
