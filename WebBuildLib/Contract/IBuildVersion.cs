using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildFoundation;
using BuildFoundation.Contract;

namespace WebBuildLib.Contract
{
    public interface IBuildVersion
    {
        Platform CI { get; set; }
        Platform QA { get; set; }
        Platform Staging { get; set; }
        Platform Production { get; set; }
    }
}
