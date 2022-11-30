using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Parser.Infrastructure
{
    public class DefaultHtmlProvider : IHtmlProvider
    {
        public string Domain { get; }

        private string _userAgent;
        
        public DefaultHtmlProvider(IOptions<DefaultHtmlProviderOptions> options)
        {
            var _options = options.Value;
            _userAgent = _options.UserAgent;
            Domain = _options.Domain;
        }
        
        public async Task<string> GetPageHtmlAsync(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new HttpRequestException();
        }
    }
}