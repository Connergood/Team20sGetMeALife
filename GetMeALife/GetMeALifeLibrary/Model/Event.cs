using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    /// <summary>
    /// Class representation of an Event in the Database
    /// </summary>
    public class Event : DatabaseObject, IEvent
    {
        public double Accessibility { get; set; }
        public string Decsription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventEnd { get; set; }
        public DateTime EventStart { get; set; }
        public int EventTypeID { get; set; }
        public string LocationAddress { get; set; }
        public string LocationName { get; set; }
        public string Name { get; set; }
        public int Participants { get; set; }
        public double Price { get; set; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"]);
            Accessibility = Convert.ToDouble(reader["ACCESSIBILITY"].ToString());
            Decsription = reader["DECSRIPTION"].ToString();
            EventDate = Convert.ToDateTime(reader["EVENTDATE"].ToString());
            EventEnd = Convert.ToDateTime(reader["EVENTEND"].ToString());
            EventStart = Convert.ToDateTime(reader["EVENTSTART"].ToString());
            EventTypeID = Convert.ToInt32(reader["EVENTTYPEID"].ToString());
            LocationAddress = reader["LOCATIONADDRESS"].ToString();
            LocationName = reader["LOCATIONNAME"].ToString();
            Name = reader["NAME"].ToString();
            Participants = Convert.ToInt32(reader["PARTICIPANTS"].ToString());
            Price = Convert.ToDouble(reader["Price"].ToString());
        }
    }
}
