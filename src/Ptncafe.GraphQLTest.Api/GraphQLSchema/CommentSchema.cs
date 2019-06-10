using GraphQL;
using GraphQL.Types;

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