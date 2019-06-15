using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class EventType : DatabaseObject, IEventType
    {
        public string Name { get; set; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            Name = reader["NAME"].ToString();
        }
    }
}
