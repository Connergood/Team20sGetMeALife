using GraphQL.Types;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class EventTypeInputType : InputObjectGraphType
    {
        public EventTypeInputType()
        {
            Name = "eventTypeInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
