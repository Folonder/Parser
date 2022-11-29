using System.Collections.Generic;
using System.Data;
using Parser.Infrastructure;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser.Services
{
    public class HtmlHtmlParserService : IHtmlParserService
    {
        private readonly IEnumerable<IHtmlProvider> _providers;
        
        public HtmlHtmlParserService(IEnumerable<IHtmlProvider> providers)
        {
            _providers = providers;
        }
        
        public async Task<string> GetSiteTitleAsync(string url)
        {
            var provider = await FindImplementation(url);
            string title = await GetTitle(await provider.GetPageHtmlAsync(url));
            if (string.IsNullOrEmpty(title))
            {
                return "Can't find title of the page";
            }
            return title;
        }

        private Task<string> GetTitle(string html)
        {
            return Task.FromResult(Regex.Match(html, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value);
        }
        
        
        private Task<IHtmlProvider> FindImplementation(string url)
        {
            foreach (var provider in _providers)
            {
                if (url.Contains(provider.Domain))
                {
                    return Task.FromResult(provider);
                }
            }

            throw new VersionNotFoundException();
        }
    }
}
