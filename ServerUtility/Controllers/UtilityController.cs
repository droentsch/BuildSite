using NapaServerUtilityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerUtility.Controllers
{
    public class UtilityController : ApiController
    {
        // POST api/values
        public HttpResponseMessage GetCacheFlush()
        {
            CacheManager mgr = new CacheManager();

            try
            {
                mgr.ClearLocalCache();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetResetIIS()
        {
            IISManager mgr = new IISManager();

            try
            {
                mgr.resetLocalAppPools();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}