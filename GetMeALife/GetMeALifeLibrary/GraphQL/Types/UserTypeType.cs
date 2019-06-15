using GetMeALibrary.Sql;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types
{
    public class UserTypeType : ObjectGraphType<GetMeALibrary.Model.UserType>
    {
        public UserTypeType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.EventTypeID).Description("ID of the EventType this user attended.");
            Field(x => x.Occurences).Description("# of times this user atteneded this event");
            Field(x => x.UserID).Description("UserID of this UserType");
        }
    }
}
