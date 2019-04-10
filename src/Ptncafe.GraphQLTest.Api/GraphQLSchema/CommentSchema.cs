﻿using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.WebUtilities;
using Ptncafe.GraphQLTest.Api.GraphQLType;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.GraphQLSchema
{
    public class CommentSchema : Schema
    {
        public CommentSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CommentQuery>();
        }
    }

    public class CommentQuery : ObjectGraphType
    {
        public CommentQuery()
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
                    var url = "https://jsonplaceholder.typicode.com/comments";
                    return await ResolveCommentApi(url, context);
                }
            );
        }

        private static async Task<IEnumerable<Model.Comment>> ResolveCommentApi(string url, ResolveFieldContext<object> context)
        {
            var queryParams = new Dictionary<string, string>();
            var postIdArgument = context.GetArgument<int?>("postId");
            if (postIdArgument.HasValue)
            {
                queryParams.Add("postId", postIdArgument.Value.ToString());
                //goto Execute;
            }
            var idArgument = context.GetArgument<int?>("id");
            if (idArgument.HasValue)
            {
                queryParams.Add("id", idArgument.Value.ToString());
                //goto Execute;
            }

        Execute:
            url = QueryHelpers.AddQueryString(url, queryParams);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Model.Comment>>(responseString);
                return result;
            }
        }
    }
}