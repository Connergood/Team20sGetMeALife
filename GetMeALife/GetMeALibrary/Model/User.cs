using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using System;

namespace GetMeALibrary.Model
{
    /// <summary>
    /// Class representation of a User
    /// </summary>
    public class User : DatabaseObject, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            FirstName = reader["FIRSTNAME"].ToString();
            LastName = reader["LASTNAME"].ToString();
            Password = reader["PASSWORD"].ToString();
            Phone = reader["PHONE"].ToString();
            Username = reader["USERNAME"].ToString();
        }
    }
}
