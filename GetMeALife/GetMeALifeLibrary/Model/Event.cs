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
        public DateTime Eventdate { get; set; }
        public DateTime Eventend { get; set; }
        public DateTime Eventstart { get; set; }
        public int Eventtypeid { get; set; }
        public double Latitude { get; set; }
        public string Locationaddress { get; set; }
        public string Locationname { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public int Participants { get; set; }
        public double Price { get; set; }

        public override string GetInsertColumns() { return "ACCESSIBILITY, DESCRIPTION, EVENTDATE, EVENTEND, EVENTSTART, EVENTTYPEID, LATITUDE, LOCATIONADDRESS, LOCATIONNAME, LONGITUDE, NAME, PARTICIPANTS, PRICE"; }

        public override string GetInsertValues() { return $"'{Accessibility}', '{Decsription}', '{Eventdate.ToString("dd/mm/yyyy")}, { Eventend.ToString("dd/mm/yyyy HH:mm")}, { Eventstart.ToString("dd/mm/yyyy HH:mm")}, {Eventtypeid}, {Latitude}, '{Locationaddress}', '{Locationname}', {Longitude}, '{Name}', {Participants}, {Price}"; }

        public override string GetSetValues(int ID)
        {
            return $"SET ACCESSIBILITY = '{Accessibility}', " +
             $"DESCRIPTION = '{Decsription}', " +
             $"EVENTDATE = '{Eventdate.ToString("dd/mm/yyyy")}', " +
             $"EVENTEND = '{ Eventend.ToString("dd/mm/yyyy HH:mm")}', " +
             $"EVENTSTART = '{ Eventstart.ToString("dd/mm/yyyy HH:mm")}' " +
             $"LATITUDE = {Latitude}, " +
             $"LOCATIONADDRESS = '{Locationaddress}', " +
             $"LOCATIONNAME = '{Locationname}', " +
             $"LONGITUDE = {Longitude}, " +
             $"NAME = '{Name}', " +
             $"PARTICIPANTS = {Participants}, " +
             $"PRICE = {Price} " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "EVENT"; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"]);
            Accessibility = Convert.ToDouble(reader["ACCESSIBILITY"].ToString());
            Decsription = reader["DECSRIPTION"].ToString();
            Eventdate = Convert.ToDateTime(reader["EVENTDATE"].ToString());
            Eventend = Convert.ToDateTime(reader["EVENTEND"].ToString());
            Eventstart = Convert.ToDateTime(reader["EVENTSTART"].ToString());
            Eventtypeid = Convert.ToInt32(reader["EVENTTYPEID"].ToString());
            Locationaddress = reader["LOCATIONADDRESS"].ToString();
            Locationname = reader["LOCATIONNAME"].ToString();
            Name = reader["NAME"].ToString();
            Participants = Convert.ToInt32(reader["PARTICIPANTS"].ToString());
            Price = Convert.ToDouble(reader["Price"].ToString());
        }
    }
}
