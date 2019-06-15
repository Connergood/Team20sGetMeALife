using GetMeALibrary.Interface;

namespace GetMeALibrary.Model
{
    public class EventType : DatabaseObject, IEventType
    {
        public string Name { get; set; }
    }
}
