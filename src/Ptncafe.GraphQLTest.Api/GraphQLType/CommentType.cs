using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.GraphQLType
{
    public class CommentType : ObjectGraphType<Model.Comment>
    {
        public CommentType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.PostId);
            Field(x => x.Body);
            Field(x => x.Email);
        }
    }
}
