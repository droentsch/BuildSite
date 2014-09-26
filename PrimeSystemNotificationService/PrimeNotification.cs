using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PrimeSystemNotificationService
{
    public partial class PrimeNotification : ServiceBase
    {
        public PrimeNotification()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            INotificationInfo notifyInfo = new BuildNotificationInfo();
            PrimeNotificationInfoManager buildMgr = new PrimeNotificationInfoManager(notifyInfo);
        }
        //Start() is for interactive debugging ONLY
        public void Start()
        {
            INotificationInfo notifyInfo = new BuildNotificationInfo();
            PrimeNotificationInfoManager buildMgr = new PrimeNotificationInfoManager(notifyInfo);
        }
        protected override void OnStop()
        {
        }
    }
}
