using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WebBuildLib.State
{
    [Serializable, XmlRoot("Logs")]
    public class Logs : List<Log>
    { }
    public class Log
    {
        public string TimeStamp { get; set; }
        public string Message { get; set; }
    }
}
