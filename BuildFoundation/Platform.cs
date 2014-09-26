using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildFoundation.Contract;

namespace BuildFoundation
{
    [Serializable]
    public class Platform : IPlatform
    {
        public ItemInfo PublicWeb{get;set;}
        public ItemInfo Xigen { get; set; }
        public ItemInfo CmsService { get; set; }
        public ItemInfo PublicService { get; set; }
        public ItemInfo CmsWeb { get; set; }
        public ItemInfo MvcWeb { get; set; }
    }
}
