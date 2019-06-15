using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class UserType : DatabaseObject, IUserType
    {
        public int UserID { get; set; }
        public int EventTypeID { get; set; }
        public int Occurences { get; set; }

        public override string GetInsertColumns() { return "USERID, EVENTTYPEID, OCCURENCES"; }

        public override string GetInsertValues() { return $"{UserID}, {EventTypeID}, {Occurences}"; }

        public override string GetSetValues(int ID)
        {
            return $"SET USERID = {UserID}, " +
             $"EVENTTYPEID = '{EventTypeID}', " +
             $"OCCURENCES = '{Occurences}', " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "USERTYPE"; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            UserID = Convert.ToInt32(reader["USERID"].ToString());
            EventTypeID = Convert.ToInt32(reader["EVENTTYPEID"].ToString());
            Occurences = Convert.ToInt32(reader["OCCURENCES"].ToString());
        }
    }
}
