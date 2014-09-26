using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebBuildLib.Contract;
using System.Xml.Serialization;

namespace WebBuildLib
{
    public class BuildConfig : IBuildConfig
    {

        public string Level
        {
            get; set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string LogPath
        {
            get;
            set;
        }

        public string LogFilePrefix
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool IncludeInBulkPush
        {
            get;
            set;
        }
    }
    [Serializable, XmlRoot("BuildConfigs")]
    public class BuildConfigs : List<BuildConfig>
    {
    }
}
