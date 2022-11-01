using PuppeteerSharp;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace Parser.Infrastructure
{
    public class HeadlessBrowser : IDataProviderAsync
    {
        public async Task<string> GetPageHtmlAsync(string url)
        {
            await new BrowserFetcher().DownloadAsync();
            Browser browser = (Browser)await Puppeteer.LaunchAsync(new LaunchOptions{Headless = true});
            var page = await browser.NewPageAsync();
            page.DefaultTimeout = 100000;
            await page.GoToAsync(url, WaitUntilNavigation.Networkidle2);
            return await page.GetContentAsync();
        }
    }
}
