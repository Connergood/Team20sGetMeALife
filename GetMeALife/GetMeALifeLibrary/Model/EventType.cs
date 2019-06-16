using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class EventType : DatabaseObject, IEventType
    {
        public string name { get; set; }
        public override string GetInsertColumns() { return "NAME"; }

        public override string GetInsertValues() { return $"'{name}'"; }

        public override string GetSetValues(int ID)
        {
            return $"SET Name = '{name}' " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "EVENTTYPE"; }

        public override void Parse(MySqlDataReader reader)
        {
            id = Convert.ToInt32(reader["ID"].ToString());
            name = reader["NAME"].ToString();
        }
    }
}
