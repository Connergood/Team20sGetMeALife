using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALifeLibrary.GraphQL.Types.Input
{
    public class UserSettingInputType : InputObjectGraphType
    {
        public UserSettingInputType()
        {
            Name = "userSettingInput";
            Field<NonNullGraphType<StringGraphType>>("Name");
            Field<NonNullGraphType<IntGraphType>>("UserID");
            Field<NonNullGraphType<StringGraphType>>("Value");
        }
    }
}

