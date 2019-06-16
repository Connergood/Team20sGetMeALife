using GetMeALibrary.Model;
using System.Collections.Generic;
using System.Net.Http;

namespace GetMeALifeLibrary.Api
{
    /// <summary>
    /// This class functions to connect to our Backend API to get data
    /// </summary>
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

        /// <summary>
        /// Reaches out to the API to get a single object
        /// </summary>
        /// <typeparam name="T">The object we want</typeparam>
        /// <param name="url">The url of the api (including everything up to the query string)</param>
        /// <param name="type">The <see cref="DatabaseObject"/> type </param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Get<T>(string url, T type, int id) where T : DatabaseObject, new()
        {
            return Get<T>(url + type.GetGraphSingleQuery(id), $"{{\"data\":{{\"{type.GetTableName().ToLower()}\":");
        }

        /// <summary>
        /// Gets a single object from the API
        /// </summary>
        /// <typeparam name="T">The object we want</typeparam>
        /// <param name="url">The url of the api (including everything up to the query string)</param>
        /// <param name="type">The <see cref="DatabaseObject"/> type </param>
        /// <param name="whereClause">a clause to narrow the results of the query</param>
        /// <returns></returns>
        public static T Get<T>(string url, T type, string whereClause) where T : DatabaseObject, new()
        {
            return Get<T>(url + type.GetGraphSingleQuery(whereClause), $"{{\"data\":{{\"{type.GetTableName().ToLower()}\":");
        }

        /// <summary>
        /// Gets a list of objects from the API
        /// </summary>
        /// <typeparam name="T">The object we want</typeparam>
        /// <param name="url">The url of the api (including everything up to the query string)</param>
        /// <param name="type">The <see cref="DatabaseObject"/> type </param>
        /// <returns></returns>
        public static List<T> GetList<T>(string url, T type) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url + type.GetGraphManyQuery());
            var json = request.Result.Remove(request.Result.Length - 2).Replace($"{{\"data\":{{\"{type.GetTableName().ToLower()}s\":", "");
            return new T().ReadListFromJson<T>(json);
        }

        /// <summary>
        /// Tells the API to create this object somewhere
        /// </summary>
        /// <typeparam name="T">The object we want</typeparam>
        /// <param name="url">The url of the api (including everything up to the query string)</param>
        /// <param name="objToMake">The <see cref="DatabaseObject"/> to create </param>
        /// <returns>The object after it's been made</returns>
        public static T Create<T>(string url, T objToMake) where T : DatabaseObject, new()
        {
            return Get<T>(url + objToMake.GetGraphCreateQuery(), $"{{\"data\":{{\"create{objToMake.GetTableName().ToLower()}\":");
        }

        /// <summary>
        /// Tells the API to create this object somewhere
        /// </summary>
        /// <typeparam name="T">The object type we want</typeparam>
        /// <param name="url">The url of the api (including everything up to the query string)</param>
        /// <param name="objToUpdate">The <see cref="DatabaseObject"/> to create </param>
        /// <param name="identifier">The ID of the <see cref="DatabaseObject"/> </param>
        /// <returns>The object after it's been updated</returns>
        public static T Update<T>(string url, T objToUpdate, int identifier) where T : DatabaseObject, new()
        {
            var returnObj = Get<T>(url + objToUpdate.GetGraphUpdateQuery(identifier), $"{{\"data\":{{\"update{objToUpdate.GetTableName().ToLower()}\":");
            returnObj.id = identifier;
            return returnObj;
        }

        /// <summary>
        /// Basic get method that includes some JSON string cutting to make parsing easier
        /// </summary>
        /// <typeparam name="T">The object type we want</typeparam>
        /// <param name="url">The url of the api (including everything up to the query string)</param>
        /// <param name="toRemove">The string to remove from the JSON</param>
        /// <returns></returns>
        private static T Get<T>(string url, string toRemove) where T : DatabaseObject, new()
        {
            var request = WebClient.GetStringAsync(url);
            var json = request.Result.Remove(request.Result.Length - 2).Replace(toRemove, "");
            var returnObj = new T().ReadFromJson<T>(json);
            return returnObj;
        }
    }
}
