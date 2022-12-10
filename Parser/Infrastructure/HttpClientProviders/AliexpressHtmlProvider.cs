using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


namespace Parser.Infrastructure
{
    public class AliexpressHtmlProvider : IHtmlProvider
    {
        public string Domain { get; }

        private uint _maxRedirectionDeep;

        private readonly HttpClientFactory _clientFactory;

        public AliexpressHtmlProvider(HttpClientFactory clientFactory,
            IOptions<AliexpressHtmlProviderOptions> options)
        {
            _clientFactory = clientFactory;
            _maxRedirectionDeep = options.Value.MaxRedirectionDeep;
            Domain = options.Value.Domain;
        }
        
        public  async Task<string> GetPageHtmlAsync(string url)
        {
            using (var response = await _clientFactory.CreateClient().GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                if ((int)response.StatusCode == 302)
                {
                    return await SolveRedirectionLoopAsync(url);
                }

                throw new HttpRequestException(response.ToString());
            }

        }

        private async Task<string> SolveRedirectionLoopAsync(string url)
        {
            HttpClient client = _clientFactory.CreateClient();
            string newUrl = url;
            
            for (int i = 0; i < _maxRedirectionDeep; i++)
            {
                // некторые сайты не уходят в цикл перенаправления, когда подключаешься к первой ссылки перенаправления,
                // при этом нужно создать новый клиент
                if (i < 2)
                {
                    client = _clientFactory.NoRedirectionClient();
                }

                using (var response = await client.GetAsync(newUrl))
                {
                    if (response.Headers.Location == null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }

                        throw new HttpRequestException(response.ToString());
                    }
                    newUrl = response.Headers.Location.ToString();
                }
            }

            throw new HttpRequestException("Can't connect to the page");
        }
    }
}