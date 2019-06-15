using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GetMeALifeLibrary.GraphQL.Types.Get;
using GetMeALifeLibrary.GraphQL.Types.Input;
using GraphQL;
using GraphQL.Types;
using System.Linq;

namespace GetMeALifeLibrary.GraphQL.GraphQLQueries
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(Database dbo)
        {
            Field<UserGetType>(
               "createUser",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
               resolve: context =>
               {
                   var user = context.GetArgument<User>("user");
                   return dbo.Insert<User>("USER", "USERNAME, PASSWORD, PHONE, FIRSTNAME, LASTNAME", $"'{user.Username}', '{user.Password}', '{user.Phone}', '{user.FirstName}', '{user.LastName}'");
               }
            );
            Field<UserGetType>(
               "updateUser",
               arguments: new QueryArguments(
                            new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" },
                            new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userID" }),
               resolve: context =>
               {
                   var updatedUser = context.GetArgument<User>("user");
                   var userID = context.GetArgument<int>("userID");

                   var dbUser = dbo.Query<User>("SELECT * FROM USER WHERE ID = " + userID).FirstOrDefault();

                   if(dbUser == null || dbUser.ID < 0)
                   {
                       context.Errors.Add(new ExecutionError("Failed to find user for ID " + updatedUser.ID));
                       return null;
                   }

                   return dbo.Update<User>("USER",
                       $"SET USERNAME = '{updatedUser.Username}', " +
                       $"PASSWORD = '{updatedUser.Password}', " +
                       $"PHONE = '{updatedUser.Phone}', " +
                       $"FIRSTNAME = '{updatedUser.FirstName}', " +
                       $"LASTNAME = '{updatedUser.LastName}' " +
                       $"WHERE ID = {userID}");
               }
            );
        }
    }
}
