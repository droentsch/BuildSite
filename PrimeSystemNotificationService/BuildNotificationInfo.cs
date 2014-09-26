
namespace PrimeSystemNotificationService
{
    public class BuildNotificationInfo : INotificationInfo
    {
        public string Name { get; set; }
        public string ConfigPath
        {
            get;
            set;
        }
        public NotificationConfig Config
        {
            get;
            set;
        }
        public PrimeEmail PrimeMail { get; set; }

        public BuildNotificationInfo()
        {
            Name = "Build Notification Service";
            ConfigPath = "Config\\BuildConfig.xml";
        }
    }
}
