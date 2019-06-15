using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class UserSetting : DatabaseObject, IUserSetting
    {
        public int UserID { get; set; }
        public string Value { get; set; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            UserID = Convert.ToInt32(reader["USERID"].ToString());
            Value = reader["VALUE"].ToString();
        }
    }
}
