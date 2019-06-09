using GraphQL.Subscription;
using GraphQL.Types;
using Microsoft.AspNetCore.WebUtilities;
using Ptncafe.GraphQLTest.Api.Model;
using Ptncafe.GraphQLTest.Proxy;
using Ptncafe.GraphQLTest.Proxy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.GraphQLSchema
{
    public class CommentQuery : ObjectGraphType
    {
        public CommentQuery(IConmentProxy conmentProxy)
        {
            FieldAsync<ListGraphType<CommentType>>("comments",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "id"
                    },
                    new QueryArgument<GraphQL.Types.StringGraphType>
                    {
                        Name = "name"
                    },
                    new QueryArgument<GraphQL.Types.StringGraphType>
                    {
                        Name = "email"
                    },
                    //new QueryArgument<GraphQL.Types.StringGraphType>
                    //{
                    //    Name = "body"
                    //},
                    new QueryArgument<GraphQL.Types.IntGraphType>
                    {
                        Name = "postId"
                    }
                }),
                resolve: async context =>
                {
                    var queryParams = new Dictionary<string, string>();
                    var postIdArgument = context.GetArgument<int?>("postId");
                    if (postIdArgument.HasValue)
                    {
                        queryParams.Add("postId", postIdArgument.Value.ToString());
                    }
                    var idArgument = context.GetArgument<int?>("id");
                    if (idArgument.HasValue)
                    {
                        queryParams.Add("id", idArgument.Value.ToString());
                    }
                    return await conmentProxy.GetComments(queryParams, context.CancellationToken);

                }
            );
        }
    }
}
