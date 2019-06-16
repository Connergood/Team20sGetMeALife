using GetMeALibrary.Model;
using System.Collections.Generic;
using System.Net.Http;

namespace GetMeALifeLibrary.Api
{
    public static class Api
    {
        private static HttpClient WebClient
        {
            get
            {
                if (_WebClient == null)
                    _WebClient = new HttpClient();
                return _WebClient;
            }
        }
        private static HttpClient _WebClient;

        public static T Get<T>(string url, T type, int id) where T : DatabaseObject, new()
        {
            return Get<T>(url + type.GetGraphSingleQuery(id), $"{{\"data\":{{\"{type.GetTableName().ToLower()}\":");
        }

        public static T Get<T>(string url, T type, string whereClause) where T : DatabaseObject, new()
        {
            return Get<T>(url + type.GetGraphSingleQuery(whereClause), $"{{\"data\":{{\"{type.GetTableName().ToLower()}\":");
        }

        public static List<T> GetList<T>(string url, T type) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url + type.GetGraphManyQuery());
            var json = request.Result.Remove(request.Result.Length - 2).Replace($"{{\"data\":{{\"{type.GetTableName().ToLower()}s\":", "");
            return new T().ReadListFromJson<T>(json);
        }

        public static T Create<T>(string url, T objToMake) where T : DatabaseObject, new()
        {
            return Get<T>(url + objToMake.GetGraphCreateQuery(), $"{{\"data\":{{\"create{objToMake.GetTableName().ToLower()}\":");
        }

        public static T Update<T>(string url, T objToUpdate, int identifier) where T : DatabaseObject, new()
        {
            var returnObj = Get<T>(url + objToUpdate.GetGraphUpdateQuery(identifier), $"{{\"data\":{{\"update{objToUpdate.GetTableName().ToLower()}\":");
            returnObj.id = identifier;
            return returnObj;
        }

        private static T Get<T>(string url, string toRemove) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url);
            var json = request.Result.Remove(request.Result.Length - 2).Replace(toRemove, "");
            var returnObj = new T().ReadFromJson<T>(json);
            return returnObj;
        }
    }
}
