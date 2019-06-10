using Ptncafe.GraphQLTest.Proxy.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Proxy
{
    public interface ICommentProxy
    {
        Task<Comment> AddUpdateComments(Comment requestData, CancellationToken cancellationToken);

        Task<Comment> GetCommentById(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Comment>> GetComments(Dictionary<string, string> queryParams, CancellationToken cancellationToken);
    }
}