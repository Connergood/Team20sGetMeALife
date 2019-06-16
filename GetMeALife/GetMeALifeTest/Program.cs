using GetMeALibrary.Model;
using GetMeALifeLibrary.Api;
using Newtonsoft.Json;
using System;

namespace GetMeALifeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var json = "{\"data\":{\"user\":[{\"firstName\":\"Alex\",\"lastName\":\"Huffman\",\"username\":\"AHDefault\",\"password\":\"HUFF\",\"phone\":\"7165554321\"}]}}";
            var jsonNeedsToBe = "{\"user\":{\"firstname\":\"bob\",\"lastname\":\"best\",\"password\":\"ferret\",\"phone\":\"555-555-5555\",\"username\":\"bobby\",\"id\":7}}";

            var user1 = new User()
            {
                Username = "Bobby",
                Firstname = "Bob",
                Lastname = "Best",
                ID = 7,
                Password = "Ferret",
                Phone = "555-555-5555"
            };

            var jsonDeserialized = JsonConvert.SerializeObject(user1);

            var user = Api.Get<User>("https://localhost:44376/graphql", new User(), 7);

            //var user = User.ReadFromJson<User>(jsonNeedsToBe);
        }
    }
}
