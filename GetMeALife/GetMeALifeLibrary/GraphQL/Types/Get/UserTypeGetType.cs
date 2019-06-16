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
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.Eventtypeid).Description("ID of the EventType this user attended.");
            Field(x => x.Occurences).Description("# of times this user atteneded this event");
            Field(x => x.Userid).Description("UserID of this UserType");
        }
    }
}
