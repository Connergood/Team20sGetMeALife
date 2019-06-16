using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    public class UserType : DatabaseObject, IUserType
    {
        public int userid { get; set; }
        public int eventtypeid { get; set; }
        public int occurences { get; set; }

        public override string GetInsertColumns() { return "USERID, EVENTTYPEID, OCCURENCES"; }

        public override string GetInsertValues() { return $"{userid}, {eventtypeid}, {occurences}"; }

        public override string GetSetValues(int ID)
        {
            return $"SET USERID = {userid}, " +
             $"EVENTTYPEID = '{eventtypeid}', " +
             $"OCCURENCES = '{occurences}', " +
             $"WHERE ID = {ID}";
        }

        public override string GetTableName() { return "USERTYPE"; }

        public override void Parse(MySqlDataReader reader)
        {
            id = Convert.ToInt32(reader["ID"].ToString());
            userid = Convert.ToInt32(reader["USERID"].ToString());
            eventtypeid = Convert.ToInt32(reader["EVENTTYPEID"].ToString());
            occurences = Convert.ToInt32(reader["OCCURENCES"].ToString());
        }
    }
}
