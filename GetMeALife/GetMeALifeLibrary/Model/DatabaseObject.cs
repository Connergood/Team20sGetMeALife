using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace GetMeALibrary.Model
{
    public abstract class DatabaseObject
    {
        public int ID { get; set; }
        public abstract void Parse(MySqlDataReader reader);
        public string GetGraphSingleQuery(int identifier)
        {
            var query = $"?query={{{this.GetTableName().ToLower()}({this.GetTableName().ToLower()}ID: {identifier}) {{ ID, {this.GetInsertColumns().ToLower()} }}}}";
            return query;
        }
        public abstract string GetInsertColumns();
        public abstract string GetInsertValues();
        public abstract string GetSetValues(int ID);
        public abstract string GetTableName();
        public T ReadFromJson<T>(string jsonString) where T : DatabaseObject, new()
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
