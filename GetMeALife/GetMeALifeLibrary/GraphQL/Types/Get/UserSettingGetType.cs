using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types.Get
{
    public class UserSettingGetType : ObjectGraphType<UserSetting>
    {
        public UserSettingGetType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.Userid).Description("The ID of the user this setting is for");
            Field(x => x.Value).Description("The Value of this setting");
        }
    }
}
