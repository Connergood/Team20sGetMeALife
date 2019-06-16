using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GetMeALifeLibrary.GraphQL.Types.Get;
using GetMeALifeLibrary.GraphQL.Types.Input;
using GraphQL;
using GraphQL.Types;
using System.Linq;

namespace GetMeALifeLibrary.GraphQL.GraphQLQueries
{
    /// <summary>
    /// Class representation of all Mutations we can do in our Schema
    /// </summary>
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(Database dbo)
        {
            #region CreateMethods

            RegisterCreateMethod<User, UserGetType, UserInputType>(dbo, "createuser", "user");
            RegisterCreateMethod<UserType, UserTypeGetType, UserTypeInputType>(dbo, "createusertype", "usertype");
            RegisterCreateMethod<UserSetting, UserSettingGetType, UserSettingInputType>(dbo, "createusersetting", "usersetting");
            RegisterCreateMethod<Event, EventGetType, EventInputType>(dbo, "createevent", "event");
            RegisterCreateMethod<EventType, EventTypeGetType, EventTypeInputType>(dbo, "createeventtype", "eventtype");

            #endregion CreateMethods

            #region UpdateMethods

            RegisterUpdateMethod<User, UserGetType, UserInputType>(dbo, "updateuser", "user", "userID");
            RegisterUpdateMethod<UserType, UserTypeGetType, UserTypeInputType>(dbo, "updateusertype", "usertype", "usertypeID");
            RegisterUpdateMethod<UserSetting, UserSettingGetType, UserSettingInputType>(dbo, "updateusersetting", "usersetting", "usersettingID");
            RegisterUpdateMethod<Event, EventGetType, EventInputType>(dbo, "updateevent", "event", "eventID");
            RegisterUpdateMethod<EventType, EventTypeGetType, EventTypeInputType>(dbo, "updateeventtype", "eventtype", "eventtypeID");
            
            #endregion UpdateMethods
        }

        /// <summary>
        /// Registers a creation method for a class
        /// </summary>
        /// <typeparam name="T">The ACTUAL object type we are using (ex:<see cref="User"/>)</typeparam>
        /// <typeparam name="U">The GET object type for the object we are using (ex:<see cref="UserGetType"/>)</typeparam>
        /// <typeparam name="V">The INPUT object type for the object we are using (ex:<see cref="UserGetType"/>)</typeparam>
        private void RegisterCreateMethod<T,U,V>(Database dbo, string methodName, string objectName) 
                                                               where T : DatabaseObject, new()
                                                               where U : ObjectGraphType<T>
                                                               where V : InputObjectGraphType
        {
            Field<U>(
               methodName,
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<V>> { Name = objectName }),
               resolve: context =>
               {
                   var objectToCreate = context.GetArgument<T>(objectName);
                   return dbo.Insert<T>(objectToCreate.GetTableName(), objectToCreate.GetInsertColumns(), objectToCreate.GetInsertValues());
               }
            );
        }

        /// <summary>
        /// Registers an update method for a class
        /// </summary>
        /// <typeparam name="T">The ACTUAL object type we are using (ex:<see cref="User"/>)</typeparam>
        /// <typeparam name="U">The GET object type for the object we are using (ex:<see cref="UserGetType"/>)</typeparam>
        /// <typeparam name="V">The INPUT object type for the object we are using (ex:<see cref="UserGetType"/>)</typeparam>
        private void RegisterUpdateMethod<T, U, V>(Database dbo, string methodName, string objectName, string objectIDName)
                                                                                        where T : DatabaseObject, new()
                                                                                        where U : ObjectGraphType<T>
                                                                                        where V : InputObjectGraphType
        {
            Field<U>(
               methodName,
               arguments: new QueryArguments(
                            new QueryArgument<NonNullGraphType<V>> { Name = objectName },
                            new QueryArgument<NonNullGraphType<IdGraphType>> { Name = objectIDName }),
               resolve: context =>
               {
                   var updatedObject = context.GetArgument<T>(objectName);
                   var objectID = context.GetArgument<int>(objectIDName);

                   var dbObject = dbo.Query<T>("SELECT * FROM "+ updatedObject.GetTableName() + " WHERE ID = " + objectID).FirstOrDefault();

                   if (dbObject == null || dbObject.id < 0)
                   {
                       context.Errors.Add(new ExecutionError($"Failed to find {objectName} for ID " + objectID));
                       return null;
                   }

                   return dbo.Update<T>(updatedObject.GetTableName(), updatedObject.GetSetValues(objectID), objectID);
               }
            );
        }
    }
}
