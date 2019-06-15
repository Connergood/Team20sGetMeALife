using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types
{
    public class UserSettingType : ObjectGraphType<UserSetting>
    {
        public UserSettingType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.UserID).Description("The ID of the user this setting is for");
            Field(x => x.Value).Description("The Value of this setting");
        }
    }
}
