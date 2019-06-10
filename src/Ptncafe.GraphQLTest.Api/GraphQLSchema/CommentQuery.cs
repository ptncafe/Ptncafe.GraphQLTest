using GraphQL.Subscription;
using GraphQL.Types;
using Microsoft.AspNetCore.WebUtilities;
using Ptncafe.GraphQLTest.Api.Infractstructure;
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
        public CommentQuery(ICommentProxy conmentProxy)
        {
            FieldAsync<ListGraphType<CommentType>>(nameof(CommentType).FirstCharToLower(),
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = nameof(Comment.Id).FirstCharToLower()
                    },
                    new QueryArgument<GraphQL.Types.StringGraphType>
                    {
                        Name = nameof(Comment.Name).FirstCharToLower()
                    },
                    new QueryArgument<GraphQL.Types.StringGraphType>
                    {
                        Name =nameof(Comment.Email).FirstCharToLower()
                    },
                    new QueryArgument<GraphQL.Types.IntGraphType>
                    {
                        Name = nameof(Comment.PostId).FirstCharToLower()
                    }
                }),
                resolve: async context =>
                {
                    var queryParams = new Dictionary<string, string>();
                    var postIdArgument = context.GetArgument<int?>(nameof(Comment.PostId).FirstCharToLower());
                    if (postIdArgument.HasValue)
                    {
                        queryParams.Add(nameof(Comment.PostId).FirstCharToLower(), postIdArgument.Value.ToString());
                    }
                    var idArgument = context.GetArgument<int?>(nameof(Comment.Id).FirstCharToLower());
                    if (idArgument.HasValue)
                    {
                        queryParams.Add(nameof(Comment.Id).FirstCharToLower(), idArgument.Value.ToString());
                    }
                    return await conmentProxy.GetComments(queryParams, context.CancellationToken);

                }
            );
        }
    }
}
