using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeSystemNotificationService
{
    public interface INotificationInfo
    {
        string ConfigPath { get; set; }
        NotificationConfig Config { get; set; }
        string Name { get; set; }
        PrimeEmail PrimeMail { get; set; }
    }
}
