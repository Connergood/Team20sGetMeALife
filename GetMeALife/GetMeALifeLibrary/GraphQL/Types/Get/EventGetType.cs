using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types.Get
{
    public class EventGetType : ObjectGraphType<Event>
    {
        public EventGetType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.Accessibility).Description("How accessible the event is 0-1");
            Field(x => x.Decsription).Description("The description of the event");
            Field(x => x.Eventdate).Description("The Date of the event");
            Field(x => x.Eventend).Description("The End Time of the event");
            Field(x => x.Eventstart).Description("The start time of the event");
            Field(x => x.Locationaddress).Description("The string address of the event location");
            Field(x => x.Locationname).Description("The name of the location");
            Field(x => x.Latitude).Description("The latitude of the event");
            Field(x => x.Longitude).Description("The longitude of the event");
            Field(x => x.Name).Description("The name of the event");
            Field(x => x.Participants).Description("The # of participants for the event");
            Field(x => x.Price).Description("the price of the event");
            Field<ListGraphType<EventTypeGetType>>(
                "type",
                resolve: context => dbo.Query<EventType>($"SELECT * FROM EVENTTYPE WHERE ID = {context.Source.Eventtypeid}")
            );
        }
    }
}
