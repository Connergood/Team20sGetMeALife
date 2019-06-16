using GetMeALibrary.Model;
using GetMeALifeLibrary.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetMeALifeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var user1 = new User()
            {
                username = "Bobby",
                firstname = "Bob",
                lastname = "Best",
                password = "Ferret",
                phone = "555-555-5555"
            };

            List<User> users2 = new List<User> { user1, user1 };
            var listJson = JsonConvert.SerializeObject(users2);

            const string graphUrl = "https://localhost:44376/graphql";

            var users = Api.GetList<User>(graphUrl, new User());
            
            var user = Api.Get<User>(graphUrl, new User(), users.FirstOrDefault().id);

            user.username = user.username + " UPDATED";

            var updatedUser = Api.Update<User>(graphUrl, user, user.id);

            var createdUser = Api.Create<User>(graphUrl, user1);

            //var user = User.ReadFromJson<User>(jsonNeedsToBe);
        }
    }
}
