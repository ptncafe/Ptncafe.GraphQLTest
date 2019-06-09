using GraphQL.Types;
using Ptncafe.GraphQLTest.Api.Model;
using Ptncafe.GraphQLTest.Proxy;
using Ptncafe.GraphQLTest.Proxy.Dto;


namespace Ptncafe.GraphQLTest.Api.GraphQLSchema
{
    public class CommentMutation : ObjectGraphType
    {
        public CommentMutation(IConmentProxy conmentProxy)
        {
            FieldAsync<CommentType>(
              "createcomment",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<CommentInputType>> { Name = "commentInput" }
              ),
               resolve: async context =>
               {
                   var requestData = context.GetArgument<Comment>("commentInput");

                   if (requestData == null)
                   {
                       throw new GraphQL.ExecutionError("Vui lòng nhập dữ liệu");
                   }
                   var response = await conmentProxy.AddUpdateComments(requestData, context.CancellationToken);

                   return response;
               });
        }
    }
}