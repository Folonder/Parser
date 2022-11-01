using Parser.Infrastructure;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser.Services
{
    public class HtmlParser : IParser
    {
        private readonly IDataProviderAsync _provider;
        public HtmlParser(IDataProviderAsync provider)
        {
            _provider = provider;
        }
        public async Task<string> GetSiteTitleAsync(string url)
        {
            string htmlCode = await _provider.GetPageHtmlAsync(url);
            string title = Regex.Match(htmlCode, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;
            if (string.IsNullOrEmpty(title))
            {
                return "Can't find title of the page";
            }
            return title;

        }
    }
}
