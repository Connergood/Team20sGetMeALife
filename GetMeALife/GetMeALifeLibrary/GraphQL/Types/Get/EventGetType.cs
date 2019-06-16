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
            Field(x => x.id, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.accessibility).Description("How accessible the event is 0-1");
            Field(x => x.description).Description("The description of the event");
            Field(x => x.eventdate).Description("The Date of the event");
            Field(x => x.eventend).Description("The End Time of the event");
            Field(x => x.eventstart).Description("The start time of the event");
            Field(x => x.eventtypeid).Description("the id of the type of event this is");
            Field(x => x.locationaddress).Description("The string address of the event location");
            Field(x => x.locationname).Description("The name of the location");
            Field(x => x.latitude).Description("The latitude of the event");
            Field(x => x.longitude).Description("The longitude of the event");
            Field(x => x.name).Description("The name of the event");
            Field(x => x.participants).Description("The # of participants for the event");
            Field(x => x.price).Description("the price of the event");
            Field<ListGraphType<EventTypeGetType>>(
                "type",
                resolve: context => dbo.Query<EventType>($"SELECT * FROM EVENTTYPE WHERE ID = {context.Source.eventtypeid}")
            );
        }
    }
}
