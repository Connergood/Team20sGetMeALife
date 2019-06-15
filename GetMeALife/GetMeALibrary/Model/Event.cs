using GetMeALibrary.Interface;
using System;

namespace GetMeALibrary.Model
{
    /// <summary>
    /// Class representation of an Event in the Database
    /// </summary>
    public class Event : DatabaseObject, IEvent
    {
        public double Accessibility { get; set; }
        public string Decsription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventEnd { get; set; }
        public DateTime EventStart { get; set; }
        public int EventTypeID { get; set; }
        public string LocationAddress { get; set; }
        public string LocationName { get; set; }
        public string Name { get; set; }
        public int Participants { get; set; }
        public double Price { get; set; }
    }
}
