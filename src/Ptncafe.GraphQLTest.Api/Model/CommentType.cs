using GraphQL.Types;
using Ptncafe.GraphQLTest.Proxy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.Model
{
    public class CommentType : ObjectGraphType<Comment>
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
