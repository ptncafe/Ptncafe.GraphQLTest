using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Ptncafe.GraphQLTest.Proxy.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Proxy
{
    public class ConmentProxy : IConmentProxy
    {
        private string _url;
        private HttpClient _httpClient;
        private ILogger<ConmentProxy> _logger;
        public ConmentProxy(HttpClient httpClient, ILogger<ConmentProxy> logger)
        {
            _httpClient = httpClient;
            _url = "https://jsonplaceholder.typicode.com/comments";
            _logger = logger;
        }

        public async Task<IEnumerable<Comment>> GetComments(Dictionary<string, string> queryParams, CancellationToken cancellationToken)
        {
            var url = QueryHelpers.AddQueryString(_url, queryParams);

            var response = await _httpClient.GetAsync(url, cancellationToken);
            _logger.LogInformation("GetComments {0} => {1}", url, response);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Comment>>(responseString);
            return result;
        }

        public async Task<Comment> AddUpdateComments(Comment requestData, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            if (requestData.Id == 0)
                response = await _httpClient.PostAsJsonAsync(_url, requestData, cancellationToken);
            else
                response = await _httpClient.PutAsJsonAsync(_url, requestData, cancellationToken);

            _logger.LogInformation("AddUpdateComments {0} => {1}", _url, response);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Comment>(responseString);
            return result;
        }
    }
}