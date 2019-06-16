using System;

namespace GetMeALibrary.Interface
{
    public interface IEvent
    {
        double Accessibility { get; set; }
        string Decsription { get; set; }
        DateTime Eventdate { get; set; }
        DateTime Eventend { get; set; }
        DateTime Eventstart { get; set; }
        int Eventtypeid { get; set; }
        double Latitude { get; set; }
        string Locationaddress { get; set; }
        string Locationname { get; set; }
        double Longitude { get; set; }
        string Name { get; set; }
        int Participants { get; set; }
        double Price { get; set; }
    }
}