using Microsoft.ApplicationServer.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NapaServerUtilityLib
{
    public class CacheManager
    {
        public CacheManager()
        {
        }
        public void ClearLocalCache()
        {
            DataCacheFactory factory = new DataCacheFactory();
            DataCache cache = factory.GetCache("PrimeData");
            foreach (string regionName in cache.GetSystemRegions())
            {
                cache.ClearRegion(regionName);
            }
        }
        public async void ClearRemoteCache(string clear_cache_link_uri)
        {
            string cclu = clear_cache_link_uri;
            HttpClient client = new HttpClient();

            string body = await client.GetStringAsync(cclu);
        }
    }
}
