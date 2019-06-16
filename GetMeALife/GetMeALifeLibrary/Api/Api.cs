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

        public static List<T> GetList<T>(string url, T type) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url + type.GetGraphManyQuery());
            var json = request.Result.Remove(request.Result.Length - 2).Replace($"{{\"data\":{{\"{type.GetTableName().ToLower()}s\":", "");
                                                                                //{"data":{"users":
            return new T().ReadListFromJson<T>(json);
        }

        public static T Create<T>(string url, T objToMake) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url + objToMake.GetGraphCreateQuery());
            var json = request.Result.Remove(request.Result.Length - 2).Replace($"{{\"data\":{{\"create{objToMake.GetTableName().ToLower()}\":", "");
            var returnObj = new T().ReadFromJson<T>(json);
            return returnObj;
        }

        public static T Update<T>(string url, T objToUpdate, int identifier) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url + objToUpdate.GetGraphUpdateQuery(identifier));
            var json = request.Result.Remove(request.Result.Length - 2).Replace($"{{\"data\":{{\"update{objToUpdate.GetTableName().ToLower()}\":", "");
            var returnObj = new T().ReadFromJson<T>(json);
            returnObj.id = identifier;
            return returnObj;
        }
    }
}
