using GraphQL.Types;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class UserTypeInputType : InputObjectGraphType
    {
        public UserTypeInputType()
        {
            Name = "userTypeInput";
            Field<NonNullGraphType<IntGraphType>>("userID");
            Field<NonNullGraphType<IntGraphType>>("eventTypeID");
            Field<NonNullGraphType<IntGraphType>>("occurences");
        }
    }
}
