using GraphQL.Types;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class EventInputType : InputObjectGraphType
    {
        public EventInputType()
        {
            Name = "eventInput";
            Field<NonNullGraphType<StringGraphType>>("accessibility");
            Field<NonNullGraphType<StringGraphType>>("decsription");
            Field<NonNullGraphType<DateTimeGraphType>>("eventDate");
            Field<NonNullGraphType<DateTimeGraphType>>("eventEnd");
            Field<NonNullGraphType<DateTimeGraphType>>("eventStart");
            Field<NonNullGraphType<IntGraphType>>("eventTypeId");
            Field<NonNullGraphType<StringGraphType>>("locationAddress");
            Field<NonNullGraphType<StringGraphType>>("locationName");
            Field<NonNullGraphType<FloatGraphType>>("latitude");
            Field<NonNullGraphType<FloatGraphType>>("longitude");
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<IntGraphType>>("participants");
            Field<NonNullGraphType<FloatGraphType>>("price");
        }
    }
}
