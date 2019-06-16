using GraphQL.Types;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class EventInputType : InputObjectGraphType
    {
        public EventInputType()
        {
            Name = "eventInput";
            Field<NonNullGraphType<StringGraphType>>("accessibility");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<NonNullGraphType<DateTimeGraphType>>("eventdate");
            Field<NonNullGraphType<DateTimeGraphType>>("eventend");
            Field<NonNullGraphType<DateTimeGraphType>>("eventstart");
            Field<NonNullGraphType<IntGraphType>>("eventtypeid");
            Field<NonNullGraphType<StringGraphType>>("locationaddress");
            Field<NonNullGraphType<StringGraphType>>("locationname");
            Field<NonNullGraphType<FloatGraphType>>("latitude");
            Field<NonNullGraphType<FloatGraphType>>("longitude");
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<IntGraphType>>("participants");
            Field<NonNullGraphType<FloatGraphType>>("price");
        }
    }
}
