using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PrimeSystemNotificationService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                var ServiceToRun = new PrimeNotification();
                ServiceToRun.Start();
                Console.WriteLine("Press enter to terminate...");
                Console.ReadLine();

                ServiceToRun.Stop();
            }
            else
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
            { 
                new PrimeNotification() 
            };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
