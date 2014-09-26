using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PrimeSystemNotificationService
{
    public class PrimeEmail
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public Body[] Bodys { get; set; }
    }

    public class Body
    {
        public string Message { get; set; }
    }
}
