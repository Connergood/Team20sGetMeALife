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
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        
        public override string GetInsertColumns() { return "USERNAME, PASSWORD, PHONE, FIRSTNAME, LASTNAME"; }
        
        public override string GetInsertValues() { return $"'{username}', '{password}', '{phone}', '{firstname}', '{lastname}'"; }

        public override string GetSetValues(int ID) { return $"SET USERNAME = '{username}', " +
                                                       $"PASSWORD = '{password}', " +
                                                       $"PHONE = '{phone}', " +
                                                       $"FIRSTNAME = '{firstname}', " +
                                                       $"LASTNAME = '{lastname}' " +
                                                       $"WHERE ID = {ID}"; }

        public override string GetTableName() { return "USER"; }

        public override void Parse(MySqlDataReader reader)
        {
            id = Convert.ToInt32(reader["ID"].ToString());
            firstname = reader["FIRSTNAME"].ToString();
            lastname = reader["LASTNAME"].ToString();
            password = reader["PASSWORD"].ToString();
            phone = reader["PHONE"].ToString();
            username = reader["USERNAME"].ToString();
        }

        public override string ToString()
        {
            return $"Name:{firstname} {lastname}, Phone:{phone}, Username({username})";
        }
    }
}
