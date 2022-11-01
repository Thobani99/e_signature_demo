//using Newtonsoft.Json;
//using RestSharp;
//using RestSharp.Serializers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CloudFlareUpload.Models.Utilities
//{
//    public static class BaseClient
//    {
//        [Obsolete]
//        public static async Task<T> ExecuteAsync<T, TS>(Method method, string uriResource, TS? data = null,
//            List<Parameter>? headerParam = null)
//            where T : class
//            where TS : class
//        {
//            var request = GetRequestObject(method, uriResource, data, headerParam);
//            var client = new RestClient();
//            var response = await client.ExecuteAsync(request);
//            if (response.Content == null) throw new Exception("No results returned " + uriResource);
//            var returnData = JsonConvert.DeserializeObject<T>(response.Content);
//            if (returnData == null) throw new Exception("Null Data Returned for " + uriResource);
//            return returnData;
//        }

//        [Obsolete]
//        private static RestRequest GetRequestObject<TS>(Method method, string uriResource,
//            TS? data = null, IReadOnlyCollection<Parameter>? headerParam = null) where TS : class
//        {
//            var request = new RestRequest(uriResource, method);
//            if (headerParam != null)
//            {
//                request.AddOrUpdateParameters(headerParam);
//            }

//            if (data != null)
//            {
//                request.AddBody(data);
//            }
//            return request;
//        }

//    }
//}
