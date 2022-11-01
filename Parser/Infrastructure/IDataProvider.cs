using System.Threading.Tasks;

namespace Parser.Infrastructure
{
    public interface IDataProviderAsync
    {
        Task<string> GetPageHtmlAsync(string url);
    }
}
