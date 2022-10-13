using Parser.Infrastructure;
using System.Text.RegularExpressions;

namespace Parser.Services
{
    public class HTMLParser : IParser
    {
        private readonly IDataProvider _provider;
        public HTMLParser(IDataProvider provider)
        {
            _provider = provider;
        }
        public string getSiteTitle(string URL)
        {
            string source = _provider.getPageHTML(URL);
            string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                           RegexOptions.IgnoreCase).Groups["Title"].Value;
            return title;
        }
    }
}
