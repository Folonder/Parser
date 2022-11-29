using System.Threading.Tasks;

namespace Parser.Services
{
    public interface IParserService
    {
        public Task<string> GetSiteTitleAsync(string url);
    }
}
