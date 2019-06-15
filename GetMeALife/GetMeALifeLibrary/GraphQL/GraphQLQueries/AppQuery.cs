using GetMeALibrary.Sql;
using GraphQL.Types;
using GetMeALibrary.Model;
using GetMeALifeLibrary.GraphQL.Types.Get;

namespace GetMeALifeLibrary.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(Database context)
        {
            Field<ListGraphType<UserGetType>>(
                "users",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM USER")
                );
            Field<ListGraphType<UserSettingGetType>>(
                "userSettings",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM USERSETTING")
                );
            Field<ListGraphType<UserTypeGetType>>(
                "userTypes",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM USERTYPE")
                );
            Field<ListGraphType<EventGetType>>(
                "events",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM EVENT")
                );
            Field<ListGraphType<EventTypeGetType>>(
                "eventTypes",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM EVENTTYPE")
                );
        }
    }
}
