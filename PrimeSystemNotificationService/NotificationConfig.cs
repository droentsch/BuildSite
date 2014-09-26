
namespace PrimeSystemNotificationService
{
    public class NotificationConfig
    {
        public string LogPath
        {
            get;
            set;
        }
        public string ErrorEmailFromAddress
        { get; set; }        
        public string FlagType { get; set; }
        public string FlagFile { get; set; }
        public string NotificationForm { get; set; }
        public string NotificationFile { get; set; }
        public int Interval { get; set; }
        public bool DeleteXmlFileAfterUse { get; set; }
    }
}
