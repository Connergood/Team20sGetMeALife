using GraphQL.Types;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class UserTypeInputType : InputObjectGraphType
    {
        public UserTypeInputType()
        {
            Name = "usertypeInput";
            Field<NonNullGraphType<IntGraphType>>("userid");
            Field<NonNullGraphType<IntGraphType>>("eventtypeid");
            Field<NonNullGraphType<IntGraphType>>("occurences");
        }
    }
}
