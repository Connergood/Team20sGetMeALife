using GraphQL.Types;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "userInput";
            Field<NonNullGraphType<StringGraphType>>("firstname");
            Field<NonNullGraphType<StringGraphType>>("lastname");
            Field<NonNullGraphType<StringGraphType>>("password");
            Field<NonNullGraphType<StringGraphType>>("phone");
            Field<NonNullGraphType<StringGraphType>>("username");
        }
    }
}
