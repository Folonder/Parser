using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


namespace Parser.Infrastructure
{
    public class AliexpressHtmlProvider : IHtmlProvider
    {
        public string Domain { get; }

        private uint _maxRedirectionDeep;

        private string _userAgent;
        
        public AliexpressHtmlProvider(IOptions<AliexpressHtmlProviderOptions> options)
        {
            var _options = options.Value;
            _maxRedirectionDeep = _options.MaxRedirectionDeep;
            _userAgent = _options.UserAgent;
            Domain = _options.Domain;
        }
        
        public  async Task<string> GetPageHtmlAsync(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            if ((int)response.StatusCode == 302)
            {
                return await SolveRedirectionLoopAsync(url);
            }

            throw new HttpRequestException();
        }

        private async Task<string> SolveRedirectionLoopAsync(string url)
        {
            HttpClient client = new HttpClient();
            string newUrl = url;
            
            for (int i = 0; i < _maxRedirectionDeep; i++)
            {
                // некторые сайты не уходят в цикл перенаправления, когда подключаешься к первой ссылки перенаправления,
                // при этом нужно создать новый клиент
                if (i < 2)
                {
                    client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
                }

                var response = await client.GetAsync(newUrl);
                if (response.Headers.Location == null)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                newUrl = response.Headers.Location.ToString();
            }

            throw new HttpRequestException();
        }
    }
}