using System;
using System.Collections.Generic;
using System.Data;
using Parser.Infrastructure;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Parser.Exceptions;

namespace Parser.Services
{
    public class HtmlParserService : IHtmlParserService
    {

        private readonly IEnumerable<IHtmlProvider> _providers;
        
        public HtmlParserService(IEnumerable<IHtmlProvider> providers)
        {
            _providers = providers;
        }
        
        public async Task<string> GetSiteTitleAsync(string url)
        {
            var provider = await FindImplementation(url);

            string title = await Task.Run(async () =>
                            GetTitle(await provider.GetPageHtmlAsync(url)));
            
            if (string.IsNullOrEmpty(title))
            {
                throw new ContentNotFoundException();
            }
            return title;
        }

        private string GetTitle(string html)
        {
            return Regex.Match(html, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;
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

            throw new VersionNotFoundException("No implementation to the url");
        }
    }
}
