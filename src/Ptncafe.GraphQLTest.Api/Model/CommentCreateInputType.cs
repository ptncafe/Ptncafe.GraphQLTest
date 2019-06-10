using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.Model
{
    public class CommentCreateInputType : InputObjectGraphType
    {
        public CommentCreateInputType()
        {
            Name = "CommentCreateInput";
            Field<NonNullGraphType<GraphQL.Types.IntGraphType>>("PostId");
            Field<StringGraphType>("Name");
            Field<StringGraphType>("Email");
            Field<StringGraphType>("Body");
        }
    }

}
