using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types.Get
{
    public class UserGetType : ObjectGraphType<User>
    {
        public UserGetType(Database dbo)
        {
            Field(x => x.ID, type: typeof(IdGraphType)).Description("ID of the user");
            Field(x => x.Firstname).Description("First name of the user");
            Field(x => x.Lastname).Description("Last name of the user");
            Field(x => x.Password).Description("Password of the user");
            Field(x => x.Phone).Description("Phone of the user");
            Field(x => x.Username).Description("Username of the user");
            Field<ListGraphType<UserSettingGetType>>(
                "settings",
                resolve: context => dbo.Query<UserSetting>($"SELECT * FROM USERSETTING WHERE USERID = {context.Source.ID}")
            );
            Field<ListGraphType<UserTypeGetType>>(
                "types",
                resolve: context => dbo.Query<UserType>($"SELECT * FROM USERTYPE WHERE USERID = {context.Source.ID}")
            );
        }
    }
}
