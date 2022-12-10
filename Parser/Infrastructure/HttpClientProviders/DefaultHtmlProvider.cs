using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace Parser.Infrastructure
{
    public class DefaultHtmlProvider : IHtmlProvider
    {
        public string Domain { get; }

        private readonly HttpClientFactory _clientFactory;

        public DefaultHtmlProvider(HttpClientFactory clientFactory,
            IOptions<DefaultHtmlProviderOptions> options)
        {
            _clientFactory = clientFactory;
            Domain = options.Value.Domain;
        }
        
        public async Task<string> GetPageHtmlAsync(string url)
        {
            using (var response = await _clientFactory.CreateClient().GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                throw new HttpRequestException(response.ToString());
            }
        }
    }
}