using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class UserSetting : DatabaseObject, IUserSetting
    {
        public string Name { get; set; }
        public int userid { get; set; }
        public string value { get; set; }

        public override string GetInsertColumns() { return "NAME, USERID, VALUE"; }

        public override string GetInsertValues() { return $"'{Name}', {userid}, '{value}'"; }

        public override string GetSetValues(int ID)
        {
            return $"SET NAME = '{Name}', " +
             $"USERID = {userid}, " +
             $"VALUE = '{value}', " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "USERSETTING"; }



        public override void Parse(MySqlDataReader reader)
        {
            id = Convert.ToInt32(reader["ID"].ToString());
            Name = reader["NAME"].ToString();
            userid = Convert.ToInt32(reader["USERID"].ToString());
            value = reader["VALUE"].ToString();
        }
    }
}
