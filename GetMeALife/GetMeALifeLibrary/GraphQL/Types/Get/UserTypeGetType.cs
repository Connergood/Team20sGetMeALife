using GetMeALibrary.Sql;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types.Get
{
    public class UserTypeGetType : ObjectGraphType<GetMeALibrary.Model.UserType>
    {
        public UserTypeGetType(Database dbo)
        {
            Field(x => x.id, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.eventtypeid).Description("ID of the EventType this user attended.");
            Field(x => x.occurences).Description("# of times this user atteneded this event");
            Field(x => x.userid).Description("UserID of this UserType");
        }
    }
}
