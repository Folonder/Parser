using System.Threading.Tasks;

namespace Parser.Services
{
    public interface IHtmlParserService
    {
        public Task<string> GetSiteTitleAsync(string url);
    }
}
