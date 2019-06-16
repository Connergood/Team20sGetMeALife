using GetMeALibrary.Sql;
using GraphQL.Types;
using GetMeALibrary.Model;
using GetMeALifeLibrary.GraphQL.Types.Get;
using System.Linq;

namespace GetMeALifeLibrary.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(Database dbo)
        {
            RegisterGetManyMethod<User, UserGetType>(dbo, "users");
            RegisterGetManyMethod<UserSetting, UserSettingGetType>(dbo, "usersettings");
            RegisterGetManyMethod<UserType, UserTypeGetType>(dbo, "usertypes");
            RegisterGetManyMethod<Event, EventGetType>(dbo, "events");
            RegisterGetManyMethod<EventType, EventTypeGetType>(dbo, "eventtypes");
            RegisterGetSingleMethod<User, UserGetType>(dbo, "user", "userID");
            RegisterGetSingleMethod<UserSetting, UserSettingGetType>(dbo, "usersetting", "usersettingID");
            RegisterGetSingleMethod<UserType, UserTypeGetType>(dbo, "usertype", "usertypeID");
            RegisterGetSingleMethod<Event, EventGetType>(dbo, "event", "eventID");
            RegisterGetSingleMethod<EventType, EventTypeGetType>(dbo, "eventtype", "eventtypeID");
        }

        private void RegisterGetManyMethod<T, U>(Database dbo, string methodName) where T : DatabaseObject, new()
                                                                                 where U : ObjectGraphType<T>
        {
            Field<ListGraphType<U>>(
                methodName,
                resolve: (ResolveFieldContext<object> context) => dbo.Query<T>($"SELECT * FROM {new T().GetTableName()}")
            );
        }
        private void RegisterGetSingleMethod<T, U>(Database dbo, string methodName, string IDName) where T : DatabaseObject, new()
                                                                                                   where U : ObjectGraphType<T>
        {
            Field<U>(
                methodName,
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = IDName }),
                resolve: (ResolveFieldContext<object> context) => dbo.Query<T>($"SELECT * FROM {new T().GetTableName()} WHERE ID = {context.GetArgument<int>(IDName)}").FirstOrDefault()
            );
        }
    }
}
