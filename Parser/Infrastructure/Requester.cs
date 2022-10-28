using System.Net;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
            get { return _maxRedirectionDeep; }
        }

        private string _userAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";

        public async Task<string> getPageHTMLAsync(string url)
        {
            return await getPageAsync(url);
        }

        public async Task<string> getPageAsync(string url)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await client.GetStringAsync(url);
                }
                if ((int)response.StatusCode == 302)
                {
                    return await solveRedirectionLoopAsync(url);
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> solveRedirectionLoopAsync(string url)
        {
            string source = "can't solve redirection loop";
            HttpClient client = new HttpClient();

            for (int i = 0; i < _maxRedirectionDeep; i++)
            {
                // некторые сайты не уходят в цикл перенаправления, когда подключаешься к первой ссылки перенаправления, при этом нужно создать новый клиент
                if (i < 2)
                {
                    client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
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
