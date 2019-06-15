using GetMeALibrary.Sql;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types
{
    public class EventTypeType : ObjectGraphType<GetMeALibrary.Model.EventType>
    {
        public EventTypeType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the event type");
            Field(x => x.Name).Description("Name Identifier of the event");
        }
    }
}
