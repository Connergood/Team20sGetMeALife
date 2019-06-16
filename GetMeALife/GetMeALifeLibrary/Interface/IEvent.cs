using System;

namespace GetMeALibrary.Interface
{
    public interface IEvent
    {
        double accessibility { get; set; }
        string description { get; set; }
        DateTime eventdate { get; set; }
        DateTime eventend { get; set; }
        DateTime eventstart { get; set; }
        int eventtypeid { get; set; }
        double latitude { get; set; }
        string locationaddress { get; set; }
        string locationname { get; set; }
        double longitude { get; set; }
        string name { get; set; }
        int participants { get; set; }
        double price { get; set; }
    }
}