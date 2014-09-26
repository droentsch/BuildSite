using System;
using System.Xml.Serialization;
using BuildFoundation;
using BuildFoundation.Contract;
using WebBuildLib.Contract;

namespace WebBuildLib
{
    [Serializable, XmlRoot("Versions")]
    public class BuildVersion : IBuildVersion
    {
        public Platform CI { get; set; }
        public Platform QA { get; set; }
        public Platform Staging { get; set; }
        public Platform Production { get; set; }
        public BuildVersion()
        {
            CI = new Platform();
            QA = new Platform();
            Staging = new Platform();
            Production = new Platform();
        }
    }
}
