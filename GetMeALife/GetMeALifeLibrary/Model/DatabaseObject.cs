using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GetMeALibrary.Model
{
    public abstract class DatabaseObject
    {
        
        public int id { get; set; }
        public abstract void Parse(MySqlDataReader reader);
        public string GetGraphSingleQuery(int identifier)
        {
            var query = $"?query={{{this.GetTableName().ToLower()}({this.GetTableName().ToLower()}ID: {identifier}) {{ id, {this.GetInsertColumns().ToLower()} }}}}";
            return query;
        }
        public string GetGraphManyQuery()
        {
            var query = $"?query={{{this.GetTableName().ToLower()}s{{ id, {this.GetInsertColumns().ToLower()} }}}}";
            return query;
        }
        public string GetGraphUpdateQuery(int identifier)
        {
            var type = this.GetTableName().ToLower();
            var properties = this.GetInsertColumns().ToLower();
            var query = $"?query=mutation(${type}:{type}Input!,${type}ID:ID!){{update{type}({type}:${type},{type}ID:${type}ID){{{properties}}}}}";
            //"?query=mutation($user:userInput!,$userID:ID!){updateUser(user:$user,userID:$userID){password,phone}}
            var variables = $"&variables={{\"{type}\":{ToJson().Replace($",\"id\":{identifier}", "")},\"{type}ID\":\"{identifier}\"}}";
            //&variables={"user":{"firstName":"Alex","lastName":"Huffman","password":"HUFF","phone":"716 555 4321","username":"AHDefault"},"userID":"7"}"
            return query + variables;
        }
        public string GetGraphCreateQuery()
        {
            var type = this.GetTableName().ToLower();
            var properties = "id, " + this.GetInsertColumns().ToLower();
            var query = $"?query=mutation(${type}:{type}Input!){{create{type}({type}:${type}){{{properties}}}}}";
            //"?query=mutation($user:userInput!){createUser(user:$user){firstName,lastName,username,password,phone}}
            var variables = $"&variables={{\"{type}\":{ToJson().Replace($",\"id\":0","")}}}";
            //&variables={"user":{"firstName":"Alex","lastName":"Huffman","password":"HUFF2","phone":"716 555 4321","username":"AHDefault"}}
            return query + variables;
        }
        public abstract string GetInsertColumns();
        public abstract string GetInsertValues();
        public abstract string GetSetValues(int ID);
        public abstract string GetTableName();
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public T ReadFromJson<T>(string jsonString) where T : DatabaseObject, new()
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        public List<T> ReadListFromJson<T>(string jsonString) where T : DatabaseObject, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
    }
}
