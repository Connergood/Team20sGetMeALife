using GetMeALibrary.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace GetMeALibrary.Sql
{
    public class Database
    {
        private string ConnectionString { get; set; }

        public Database(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<T> Query<T>(string queryString) where T : DatabaseObject, new()
        {
            var returnList = new List<T>();
            using (MySqlConnection context = GetConnection())
            {
                context.Open();
                MySqlCommand cmd = new MySqlCommand(queryString, context);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new T();
                        obj.Parse(reader);
                        returnList.Add(obj);
                    }
                }
            }
            return returnList;
        }
    }
}
