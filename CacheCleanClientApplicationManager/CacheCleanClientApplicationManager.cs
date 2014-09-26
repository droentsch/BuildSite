using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheManager;
using System.Net;
using System.IO;

namespace CacheCleanClientApplication
{
    public class CacheCleanClientApplicationManager
    {
        public CacheCleanClientApplicationManager(string uri)
        {
            _uri_base = uri;
        }
        public string Clean(string group_type, string matching, string named_file)
        {
            string query = createRequest(CleanRequest.CLEAN, group_type, matching, named_file);
            string response = executeQuery(query);
            return response;
        }
        public string ListItems(string group_type, string matching, string named_file)
        {
            string query = createRequest(CleanRequest.LIST, group_type, matching, named_file);
            string response = executeQuery(query);
            return response;
        }

        #region private
        private string _uri_base;
        private string createRequest(string request_type, string group_type, string matching, string named_file)
        {
            string query = string.Empty;
            string name = string.Empty;

            if (request_type == CleanRequest.CLEAN)
            {
                name = CleanRequest.CLEAN_TYPE;
            }
            else if (request_type == CleanRequest.LIST)
            {
                name = CleanRequest.LIST_TYPE;
            }

            if (group_type == CleanRequest.MATCHING && !String.IsNullOrEmpty(matching))
            {
                query = string.Format("?{0}={1}&{2}={3}&{4}={5}", CleanRequest.REQUEST_TYPE, request_type, name, group_type, CleanRequest.MATCHING, matching);                
            }
            else if (group_type == CleanRequest.NAMED_FILE && !String.IsNullOrEmpty(named_file))
            {
                query = string.Format("?{0}={1}&{2}={3}&{4}={5}", CleanRequest.REQUEST_TYPE, request_type, name, group_type, CleanRequest.NAMED_FILE, named_file);
            }
            else
            {
                query = string.Format("?{0}={1}&{2}={3}", CleanRequest.REQUEST_TYPE, request_type, name, group_type);
            }

            return query;
        }
        private string executeQuery(string generated_query)
        {
            string query = String.Format("{0}{1}", _uri_base, generated_query);
            string response = string.Empty;
            WebRequest webrequest = WebRequest.Create(query);
            webrequest.Method = "GET";
            using (WebResponse web_response = webrequest.GetResponse())
            {
                using (Stream dataStream = web_response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        response = reader.ReadToEnd();
                    }
                }
            }
            return response; 
        }
        #endregion
    }
}
