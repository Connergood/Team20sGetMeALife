using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class EventType : DatabaseObject, IEventType
    {
        public string Name { get; set; }
        public override string GetInsertColumns() { return "NAME"; }

        public override string GetInsertValues() { return $"'{Name}'"; }

        public override string GetSetValues(int ID)
        {
            return $"SET Name = '{Name}', " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "EVENTTYPE"; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            Name = reader["NAME"].ToString();
        }
    }
}
