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

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            UserID = Convert.ToInt32(reader["USERID"].ToString());
            EventTypeID = Convert.ToInt32(reader["EVENTTYPEID"].ToString());
            Occurences = Convert.ToInt32(reader["OCCURENCES"].ToString());
        }
    }
}
