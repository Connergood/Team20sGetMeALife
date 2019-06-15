using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.FirstName).Description("First name of the user");
            Field(x => x.LastName).Description("Last name of the user");
            Field(x => x.Password).Description("Password of the user");
            Field(x => x.Phone).Description("Phone of the user");
            Field(x => x.Username).Description("Username of the user");
            Field<ListGraphType<UserSettingType>>(
                "settings",
                resolve: context => dbo.Query<UserSetting>($"SELECT * FROM USERSETTING WHERE USERID = {context.Source.ID}")
            );
            Field<ListGraphType<UserTypeType>>(
                "types",
                resolve: context => dbo.Query<UserSetting>($"SELECT * FROM USERTYPE WHERE USERID = {context.Source.ID}")
            );
        }
    }
}
