using Parser.Infrastructure;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser.Services
{
    public class HTMLParser : IParser
    {
        private readonly IDataProviderAsync _provider;
        public HTMLParser(IDataProviderAsync provider)
        {
            _provider = provider;
        }
        public async Task<string> getSiteTitleAsync(string url)
        {
            string htmlCode = await _provider.getPageHTMLAsync(url);
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
