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
            Field(x => x.id, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.userid).Description("The ID of the user this setting is for");
            Field(x => x.value).Description("The Value of this setting");
        }
    }
}
