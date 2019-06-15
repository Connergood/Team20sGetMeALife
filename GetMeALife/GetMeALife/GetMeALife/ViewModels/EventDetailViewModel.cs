using GetMeALibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALife.ViewModels
{
    public class EventDetailViewModel
    {
        public EventDetail eventDetail { get; set; }
        private bool _isConfirmed { get; set; }

        public bool isConfirmed
        {
            get { return _isConfirmed; }
        }
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
