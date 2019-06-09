using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ptncafe.GraphQLTest.Proxy.Dto;

namespace Ptncafe.GraphQLTest.Proxy
{
    public interface IConmentProxy
    {
        Task<Comment> AddUpdateComments(Comment requestData, CancellationToken cancellationToken);
        Task<IEnumerable<Comment>> GetComments(Dictionary<string, string> queryParams, CancellationToken cancellationToken);
    }
}