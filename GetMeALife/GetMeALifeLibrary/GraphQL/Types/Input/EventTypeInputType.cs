using GraphQL.Types;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class EventTypeInputType : InputObjectGraphType
    {
        public EventTypeInputType()
        {
            Name = "eventtypeInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
