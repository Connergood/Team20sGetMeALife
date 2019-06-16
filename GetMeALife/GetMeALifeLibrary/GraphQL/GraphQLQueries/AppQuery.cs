using GetMeALibrary.Sql;
using GraphQL.Types;
using GetMeALibrary.Model;
using GetMeALifeLibrary.GraphQL.Types.Get;
using System.Linq;
using System.Collections.Generic;
using GetMeALifeLibrary.BoredApi;

namespace GetMeALifeLibrary.GraphQL.GraphQLQueries
{
    /// <summary>
    /// Class reperesentation of all the object(s) we can query for in the Schema
    /// </summary>
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

        /// <summary>
        /// Registers a method that allows users to query for multiple of a single object
        /// </summary>
        /// <typeparam name="T">The ACTUAL object we will get back</typeparam>
        /// <typeparam name="U">The GET type of the object we want</typeparam>
        /// <param name="dbo">our connection to the database</param>
        /// <param name="methodName">the Graph QL name of this method</param>
        /// <remarks>
        /// For <see cref="Event"/>s we include objects from https://www.boredapi.com/
        /// </remarks>
        private void RegisterGetManyMethod<T, U>(Database dbo, string methodName) where T : DatabaseObject, new()
                                                                                 where U : ObjectGraphType<T>
        {
            Field<ListGraphType<U>>(
                methodName,
                resolve: (ResolveFieldContext<object> context) => 
                {
                    List<T> objects = dbo.Query<T>($"SELECT * FROM {new T().GetTableName()}");
                    if(objects is List<Event>)
                    {
                        foreach (var type in Activity.AllActivityTypes)
                            objects.Add(BoredApi.Api.GetActivityByType(type).ToEvent() as T);
                        // as T does nothing since we know it's an Event, it's just to hide compiler warning
                        objects.Add(BoredApi.Api.GetRandomActivity().ToEvent() as T);
                    }
                    return objects;
                }
            );
        }

        /// <summary>
        /// Registers a method that allows users to query for multiple of a single object
        /// </summary>
        /// <typeparam name="T">The ACTUAL object we will get back</typeparam>
        /// <typeparam name="U">The GET type of the object we want</typeparam>
        /// <param name="dbo">our connection to the database</param>
        /// <param name="methodName">the Graph QL name of this method</param>
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
