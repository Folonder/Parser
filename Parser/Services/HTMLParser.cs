using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Parser.Infrastructure;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser.Services
{
    public class HtmlParserService : IParserService
    {
        private readonly IEnumerable<IHtmlProvider> _providers;
        public HtmlParserService(IEnumerable<IHtmlProvider> providers)
        {
            _providers = providers;
        }
        public async Task<string> GetSiteTitleAsync(string url)
        {
            var provider = FindImplementation(url);
            string htmlCode = await provider.GetPageHtmlAsync(url);
            string title = Regex.Match(htmlCode, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;
            if (string.IsNullOrEmpty(title))
            {
                return "Can't find title of the page";
            }
            return title;

        }

        private IHtmlProvider FindImplementation(string url)
        {
            foreach (var provider in _providers)
            {
                if (url.Contains(provider.Domain))
                {
                    return provider;
                }
            }

            throw new VersionNotFoundException();
        }
    }
}
