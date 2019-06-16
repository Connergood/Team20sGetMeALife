using GetMeALibrary.Model;
using GetMeALifeLibrary.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GetMeALifeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var user1 = new User()
            //{
            //    username = "Bobby",
            //    firstname = "Bob",
            //    lastname = "Best",
            //    password = "Ferret",
            //    phone = "555-555-5555"
            //};

            //List<User> users2 = new List<User> { user1, user1 };
            //var listJson = JsonConvert.SerializeObject(users2);

            const string graphUrl = "https://localhost:44376/graphql";
            //const string graphUrl = "http://getmealife.azurewebsites.net/graphql";

            var userType = new UserType()
            {
                id = 1,
                userid = 1,
                eventtypeid = 1,
                occurrences = 3
            };
            userType = Api.Update(graphUrl, userType, userType.id);
            Console.WriteLine("yay");


            //var events = Api.GetList<Event>(graphUrl, new Event());
            //foreach (var evnt in events)
            //{
            //    Console.WriteLine(evnt.ToString());
            //}

            //var users = Api.GetList<User>(graphUrl, new User());

            //var user = Api.Get<User>(graphUrl, new User(), users.FirstOrDefault().id);

            //user.username = user.username + " UPDATED";

            //var updatedUser = Api.Update<User>(graphUrl, user, user.id);

            //var createdUser = Api.Create<User>(graphUrl, user1);

            //var file = "C:\\Users\\cwestover\\Desktop\\EventData.csv";

            //string[] csvRows = File.ReadAllLines(file);
            //List<Event> events = new List<Event>();
            //List<EventType> eventTypes = new List<EventType>();
            //bool skip = true;
            //foreach (var row in csvRows)
            //{
            //    if (skip)
            //    {
            //        skip = false;
            //        continue;
            //    }
            //    var fields = row.Split(",");
            //    var eventType = new EventType()
            //    {
            //        name = fields[9]
            //    };
            //    if (!eventTypes.Contains(eventType))
            //        eventTypes.Add(eventType);

            //    var eventTypeIndex = eventTypes.IndexOf(eventType) + 1;

            //    var evnt = new Event()
            //    {
            //        accessibility = (fields[7] == "Relaxed" ? 0.2 : 0.4) + (fields[14] == "Participant" ? 0.0 : (fields[14] == "Varies" ? .3 : .15)),
            //        description = fields[6],
            //        eventdate = DateTime.Parse(fields[2]),
            //        eventstart = DateTime.Parse(fields[2]) + TimeSpan.Parse(fields[3]),
            //        eventend = DateTime.Parse(fields[2]) + TimeSpan.Parse(fields[4]),
            //        eventtypeid = eventTypeIndex,
            //        latitude = 0.0,
            //        longitude = 0.0,
            //        locationaddress = (fields[11] + fields[12] + fields[13]).Replace("\"", ""),
            //        locationname = fields[10],
            //        name = fields[1],
            //        participants = fields[8] == "Small" ? 0 : 2,
            //        price = Double.Parse(fields[5])
            //    };

            //    events.Add(evnt);
            //}

            ////foreach (var eventType in eventTypes)
            ////{
            ////    var committedEvent = Api.Create<EventType>(graphUrl, eventType);
            ////    Console.WriteLine(committedEvent.ToJson());
            ////}

            //foreach (var evnt in events)
            //{
            //    var committedEvent = Api.Create<Event>(graphUrl, evnt);
            //    Console.WriteLine(committedEvent.ToJson());
            //}

            //var user = User.ReadFromJson<User>(jsonNeedsToBe);
        }
    }
}
