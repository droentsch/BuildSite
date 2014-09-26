using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildFoundation.Contract
{
    public interface IPlatform
    {
        ItemInfo PublicWeb { get; set; }
        ItemInfo Xigen { get; set; }
        ItemInfo CmsService { get; set; }
        ItemInfo PublicService { get; set; }
        ItemInfo CmsWeb { get; set; }
        ItemInfo MvcWeb { get; set; }
    }
}
