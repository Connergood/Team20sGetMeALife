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
            Name = "usersettingInput";
            Field<NonNullGraphType<StringGraphType>>("Name");
            Field<NonNullGraphType<IntGraphType>>("Userid");
            Field<NonNullGraphType<StringGraphType>>("Value");
        }
    }
}

