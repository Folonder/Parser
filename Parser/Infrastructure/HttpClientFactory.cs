using System.Net.Http;
using Microsoft.Extensions.Options;

namespace Parser.Infrastructure
{
    public class HttpClientFactory
    {
        private static string _userAgent;

        public HttpClientFactory(IOptions<HttpClientFactoryOptions> options)
        {
            var _options = options.Value;
            _userAgent = _options.UserAgent;
        }
        
        public HttpClient NoRedirectionClient()
        {
            var client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });
            client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
            return client;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
            return client;
        }
    }
}