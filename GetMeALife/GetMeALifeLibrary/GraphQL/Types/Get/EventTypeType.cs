using GetMeALibrary.Sql;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types.Get
{
    public class EventTypeGetType : ObjectGraphType<GetMeALibrary.Model.EventType>
    {
        public EventTypeGetType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the event type");
            Field(x => x.Name).Description("Name Identifier of the event");
        }
    }
}
