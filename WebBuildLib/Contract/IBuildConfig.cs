using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebBuildLib.Contract
{
    public interface IBuildConfig
    {
        string Level {get;set;}
        string Type { get; set; }
        string Name {get;set;}
        string Path {get;set;}
        string LogPath {get;set;}
        string LogFilePrefix {get;set;}
        string Description { get; set; }
        bool IncludeInBulkPush { get; set; }
    }
}
