using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBuildSite.Extensions
{
    public static class CookieExtension
    {
        public static void SetValue(this HttpCookieCollection cookies, string cookie_name, string value_to_set, int days_till_expiration)
        {
            var s = new HttpCookie(cookie_name);

            if (cookies[cookie_name] != null)
            {
                cookies[cookie_name].Expires = DateTime.UtcNow - TimeSpan.FromDays(1);
            }
            s.Expires = DateTime.UtcNow + TimeSpan.FromDays(days_till_expiration); ;
            s.Value = value_to_set;
            cookies.Add(s);
        }
        public static string GetValue(this HttpCookieCollection cookies, string cookie_name)
        {
            if (cookies[cookie_name] == null) return null;
            string val = cookies[cookie_name].Value;
            return val;
        }
        public static void Delete(this HttpCookieCollection cookies, string name_to_delete)
        {
            if (cookies[name_to_delete] == null) return;
            cookies[name_to_delete].Expires = DateTime.UtcNow - TimeSpan.FromDays(1);
        }
    }
}
