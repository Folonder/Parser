using System.Net.Http;
using System;
using System.Threading.Tasks;


namespace Parser.Infrastructure
{
    public class Requester : IDataProviderAsync
    {
        private int _maxRedirectionDeep = 10;

        public int MaxRedirectionDeep
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("The argument must be greater than 0");
                }

                _maxRedirectionDeep = value;
            }
            get => _maxRedirectionDeep;
        }

        public string UserAgent { get; set; } =
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";

        public async Task<string> GetPageHtmlAsync(string url)
        {
            return await GetPageAsync(url);
        }

        private async Task<string> GetPageAsync(string url)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await client.GetStringAsync(url);
                }

                if ((int)response.StatusCode == 302)
                {
                    return await SolveRedirectionLoopAsync(url);
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> SolveRedirectionLoopAsync(string url)
        {
            string source = "can't solve redirection loop";
            HttpClient client = new HttpClient();

            for (int i = 0; i < _maxRedirectionDeep; i++)
            {
                // некторые сайты не уходят в цикл перенаправления, когда подключаешься к первой ссылки перенаправления, при этом нужно создать новый клиент
                if (i < 2)
                {
                    client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
                }

                var response = await client.GetAsync(url);
                if (response.Headers.Location == null)
                {
                    return await client.GetStringAsync(url);
                }

                url = response.Headers.Location.ToString();
            }

            return source;
        }
    }
}