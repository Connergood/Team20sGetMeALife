using GetMeALibrary.Model;
using MySql.Data.MySqlClient;
using System;
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

        public T Insert<T>(string table, string columns, string values) where T : DatabaseObject, new()
        {
            try
            {
                T objectToReturn = new T();
                using (MySqlConnection context = GetConnection())
                {
                    context.Open();
                    MySqlCommand insertCmd = new MySqlCommand($"INSERT INTO {table} ({columns}) VALUES({values})", context);
                    insertCmd.ExecuteNonQuery();
                }
                using (MySqlConnection context = GetConnection())
                {
                    context.Open();
                    MySqlCommand getCmd = new MySqlCommand($"SELECT * FROM {table} ORDER BY ID DESC LIMIT 1", context);

                    using (var reader = getCmd.ExecuteReader())
                    {
                        while (reader.Read())
                            objectToReturn.Parse(reader);
                    }
                }
                return objectToReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public T Update<T>(string table, string query, int ID) where T : DatabaseObject, new()
        {
            try
            {
                T objectToReturn = new T();
                using (MySqlConnection context = GetConnection())
                {
                    context.Open();
                    MySqlCommand updateCmd = new MySqlCommand($"UPDATE {table} {query}", context);
                    updateCmd.ExecuteNonQuery();
                }
                using (MySqlConnection context = GetConnection())
                {
                    context.Open();
                    MySqlCommand getCmd = new MySqlCommand($"SELECT * FROM {table} WHERE ID = {ID}", context);

                    using (var reader = getCmd.ExecuteReader())
                    {
                        while (reader.Read())
                            objectToReturn.Parse(reader);
                    }
                }
                return objectToReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
