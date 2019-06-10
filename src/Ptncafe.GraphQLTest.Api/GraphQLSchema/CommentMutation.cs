using AutoMapper;
using GraphQL.Types;
using Ptncafe.GraphQLTest.Api.Infractstructure;
using Ptncafe.GraphQLTest.Api.Model;
using Ptncafe.GraphQLTest.Proxy;
using Ptncafe.GraphQLTest.Proxy.Dto;

namespace Ptncafe.GraphQLTest.Api.GraphQLSchema
{
    public class CommentMutation : ObjectGraphType
    {
        public CommentMutation(ICommentProxy commentProxy, IMapper mapper)
        {
            FieldAsync<CommentType>(
              "createcomment",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<CommentCreateInputType>> { Name = nameof(CommentCreateInputType).FirstCharToLower() }
              ),
               resolve: async context =>
               {
                   var requestData = context.GetArgument<Comment>(nameof(CommentCreateInputType).FirstCharToLower());

                   if (requestData == null)
                   {
                       throw new GraphQL.ExecutionError("Vui lòng nhập dữ liệu");
                   }

                   return await commentProxy.AddUpdateComments(requestData, context.CancellationToken);
               });

            FieldAsync<CommentType>(
              "updatecomment",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<CommentUpdateInputType>> { Name = nameof(CommentUpdateInputType).FirstCharToLower() }
              ),
               resolve: async context =>
               {
                   var requestData = context.GetArgument<Comment>(nameof(CommentUpdateInputType).FirstCharToLower());

                   if (requestData == null)
                   {
                       throw new GraphQL.ExecutionError("Vui lòng nhập dữ liệu");
                   }
                   var data = await commentProxy.GetCommentById(requestData.Id, context.CancellationToken);

                   //Map without null properties
                   data = mapper.Map<Comment, Comment>(requestData, data);

                   return await commentProxy.AddUpdateComments(data, context.CancellationToken);
               });
        }
    }
}