using System;

namespace GetMeALibrary.Interface
{
    public interface IEvent
    {
        double Accessibility { get; set; }
        string Decsription { get; set; }
        DateTime EventDate { get; set; }
        DateTime EventEnd { get; set; }
        DateTime EventStart { get; set; }
        int EventTypeID { get; set; }
        double Latitude { get; set; }
        string LocationAddress { get; set; }
        string LocationName { get; set; }
        double Longitude { get; set; }
        string Name { get; set; }
        int Participants { get; set; }
        double Price { get; set; }
    }
}