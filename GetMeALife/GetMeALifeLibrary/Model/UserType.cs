using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class UserType : DatabaseObject, IUserType
    {
        public int Userid { get; set; }
        public int Eventtypeid { get; set; }
        public int Occurences { get; set; }

        public override string GetInsertColumns() { return "USERID, EVENTTYPEID, OCCURENCES"; }

        public override string GetInsertValues() { return $"{Userid}, {Eventtypeid}, {Occurences}"; }

        public override string GetSetValues(int ID)
        {
            return $"SET USERID = {Userid}, " +
             $"EVENTTYPEID = '{Eventtypeid}', " +
             $"OCCURENCES = '{Occurences}', " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "USERTYPE"; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            Userid = Convert.ToInt32(reader["USERID"].ToString());
            Eventtypeid = Convert.ToInt32(reader["EVENTTYPEID"].ToString());
            Occurences = Convert.ToInt32(reader["OCCURENCES"].ToString());
        }
    }
}
