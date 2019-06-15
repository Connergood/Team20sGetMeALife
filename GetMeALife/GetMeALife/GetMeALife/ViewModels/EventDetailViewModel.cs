using GetMeALibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALife.ViewModels
{
    public class EventDetailViewModel
    {
        public EventDetail eventDetail { get; set; }

        public bool isConfirmed { get; set; }

        public EventListViewModel eventList { get; set; }
    }

    public class EventDetail : Event
    {
        public string TypeName
        {
            get
            {
                //query for typename
                return null;
            }
        }
    }
}
