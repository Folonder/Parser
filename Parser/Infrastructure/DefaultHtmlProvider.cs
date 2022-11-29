using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parser.Infrastructure
{
    public class DefaultHtmlProvider : IHtmlProvider
    {
        public string Domain { get; } = "https://";

        public string UserAgent { get; set; } =
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";

        public async Task<string> GetPageHtmlAsync(string url)
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

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}