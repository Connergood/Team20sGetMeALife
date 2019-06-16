using GetMeALibrary.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GetMeALifeLibrary.Api
{
    public static class Api
    {
        private static HttpClient WebClient {
            get
            {
                if(_WebClient == null)
                    _WebClient = new HttpClient();
                return _WebClient;
            }
        }
        private static HttpClient _WebClient;

        public static T Get<T>(string url, T type, int id) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url + type.GetGraphSingleQuery(id));
            var json = request.Result.Remove(request.Result.Length - 2).Replace($"{{\"data\":{{\"{type.GetTableName().ToLower()}\":", "");
            return new T().ReadFromJson<T>(json);
        }
    }
}
