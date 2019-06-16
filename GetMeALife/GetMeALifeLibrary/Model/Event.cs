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
        public double accessibility { get; set; }
        public string description { get; set; }
        public DateTime eventdate { get; set; }
        public DateTime eventend { get; set; }
        public DateTime eventstart { get; set; }
        public int eventtypeid { get; set; }
        public double latitude { get; set; }
        public string locationaddress { get; set; }
        public string locationname { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public int participants { get; set; }
        public double price { get; set; }

        public override string GetInsertColumns() { return "ACCESSIBILITY, DESCRIPTION, EVENTDATE, EVENTEND, EVENTSTART, EVENTTYPEID, LATITUDE, LOCATIONADDRESS, LOCATIONNAME, LONGITUDE, NAME, PARTICIPANTS, PRICE"; }

        public override string GetInsertValues() { return $"'{accessibility}', '{description}', '{eventdate.ToString("yyyy-MM-dd HH:mm:ss")}', '{ eventend.ToString("yyyy-MM-dd HH:mm:ss")}', '{ eventstart.ToString("yyyy-MM-dd HH:mm:ss")}', {eventtypeid}, {latitude}, '{locationaddress}', '{locationname}', {longitude}, '{name}', {participants}, {price}"; }

        public override string GetSetValues(int ID)
        {
            return $"SET ACCESSIBILITY = '{accessibility}', " +
             $"DESCRIPTION = '{description}', " +
             $"EVENTDATE = '{eventdate.ToString("dd/mm/yyyy")}', " +
             $"EVENTEND = '{ eventend.ToString("dd/mm/yyyy HH:mm")}', " +
             $"EVENTSTART = '{ eventstart.ToString("dd/mm/yyyy HH:mm")}' " +
             $"LATITUDE = {latitude}, " +
             $"LOCATIONADDRESS = '{locationaddress}', " +
             $"LOCATIONNAME = '{locationname}', " +
             $"LONGITUDE = {longitude}, " +
             $"NAME = '{name}', " +
             $"PARTICIPANTS = {participants}, " +
             $"PRICE = {price} " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "EVENT"; }

        public override void Parse(MySqlDataReader reader)
        {
            id = Convert.ToInt32(reader["ID"]);
            accessibility = Convert.ToDouble(reader["ACCESSIBILITY"].ToString());
            description = reader["DESCRIPTION"].ToString();
            eventdate = Convert.ToDateTime(reader["EVENTDATE"].ToString());
            eventend = Convert.ToDateTime(reader["EVENTEND"].ToString());
            eventstart = Convert.ToDateTime(reader["EVENTSTART"].ToString());
            eventtypeid = Convert.ToInt32(reader["EVENTTYPEID"].ToString());
            locationaddress = reader["LOCATIONADDRESS"].ToString();
            locationname = reader["LOCATIONNAME"].ToString();
            name = reader["NAME"].ToString();
            participants = Convert.ToInt32(reader["PARTICIPANTS"].ToString());
            price = Convert.ToDouble(reader["Price"].ToString());
        }

        public override string ToString()
        {
            return name;
        }
    }
}
