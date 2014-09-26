using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildFoundation.Contract
{
    public interface IItemInfo
    {
        String Name { get; set; }
        String Label{get;set;}
        String Created { get; set; }
        DateTime CreatedDate { get; }
    }
}
