using GetMeALibrary.Sql;
using GraphQL.Types;
using GetMeALibrary.Model;

namespace GetMeALifeLibrary.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(Database context)
        {
            Field<ListGraphType<Types.UserType>>(
                "users",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM USER")
                );
            Field<ListGraphType<Types.UserSettingType>>(
                "userSettings",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM USERSETTING")
                );
            Field<ListGraphType<Types.UserTypeType>>(
                "userTypes",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM USERTYPE")
                );
            Field<ListGraphType<Types.EventType>>(
                "events",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM EVENT")
                );
            Field<ListGraphType<Types.EventTypeType>>(
                "eventTypes",
                resolve: (ResolveFieldContext<object> ctx) => context.Query<User>("SELECT * FROM EVENTTYPE")
                );
        }
    }
}
