using GetMeALibrary.Interface;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace GetMeALibrary.Model
{
    /// <summary>
    /// Class representation of a User
    /// </summary>
    public class User : DatabaseObject, IUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        
        public override string GetInsertColumns() { return "USERNAME, PASSWORD, PHONE, FIRSTNAME, LASTNAME"; }
        
        public override string GetInsertValues() { return $"'{Username}', '{Password}', '{Phone}', '{Firstname}', '{Lastname}'"; }

        public override string GetSetValues(int ID) { return $"SET USERNAME = '{Username}', " +
                                                       $"PASSWORD = '{Password}', " +
                                                       $"PHONE = '{Phone}', " +
                                                       $"FIRSTNAME = '{Firstname}', " +
                                                       $"LASTNAME = '{Lastname}' " +
                                                       $"WHERE ID = {ID}"; }

        public override string GetTableName() { return "USER"; }

        public override void Parse(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["ID"].ToString());
            Firstname = reader["FIRSTNAME"].ToString();
            Lastname = reader["LASTNAME"].ToString();
            Password = reader["PASSWORD"].ToString();
            Phone = reader["PHONE"].ToString();
            Username = reader["USERNAME"].ToString();
        }

        public override string ToString()
        {
            return $"Name:{Firstname} {Lastname}, Phone:{Phone}, Username({Username})";
        }
    }
}
