using GetMeALibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.BoredApi
{
    public class Activity
    {
        public static string[] AllActivityTypes = { "education", "recreational", "social", "diy", "charity", "cooking", "relaxation", "music", "busywork" };

        public string activity { get; set; }
        public string accessibility { get; set; }
        public string type { get; set; }
        public int participants { get; set; }
        public double price { get; set; }
        public string link { get; set; }
        public string key { get; set; }

        public static Activity ReadFromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<Activity>(jsonString);
        }

        public Event ToEvent()
        {
            return new Event()
            {
                id = 0,
                accessibility = Convert.ToDouble(this.accessibility),
                name = activity,
                description = type + " event. For more info check out " + link,
                participants = participants >= 12 ? 2 : (participants >= 7 ? 1 : 0),
                price = price,
                latitude = 0.0,
                longitude = 0.0,
                locationaddress = "Unknown",
                locationname = "Unknown",
                eventdate = DateTime.Today,
                eventstart = DateTime.Now,
                eventend = DateTime.Now,
                eventtypeid = 1
            };
        }
    }
}
