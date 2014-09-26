using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NapaServerUtilityLib
{
    public class IISManager
    {
        public void resetLocalAppPools()
        {
            using (var manager = new ServerManager())
            {
                foreach (var pool in manager.ApplicationPools)
                {
                    if (pool.Name == "Prime")
                    {
                        Process process = null;
                        if (pool.WorkerProcesses.Count > 0)
                        {
                            process = Process.GetProcessById(pool.WorkerProcesses[0].ProcessId);
                        }
                        pool.Recycle();
                        if (process != null)
                        {
                            while (!process.HasExited)
                                Thread.Sleep(0);
                            process.Dispose();
                        }
                    }
                }
            }
        }
        public async void resetRemoteIIS(string reset_iis_uri)
        {
            string uri = reset_iis_uri;
            HttpClient client = new HttpClient();

            string body = await client.GetStringAsync(uri);
        }

    }
}
