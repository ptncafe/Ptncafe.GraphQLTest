using GraphQL;
using GraphQL.Subscription;
using GraphQL.Types;
using Microsoft.AspNetCore.WebUtilities;
using Ptncafe.GraphQLTest.Api.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.GraphQLSchema
{
    /// <summary>
    /// https://medium.com/volosoft/building-graphql-apis-with-asp-net-core-419b32a5305b
    /// </summary>
    public class CommentSchema : Schema
    {
        public CommentSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CommentQuery>();
            Mutation = resolver.Resolve<CommentMutation>();
        }
    }

    

   
}