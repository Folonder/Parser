using System.Threading.Tasks;

namespace Parser.Infrastructure
{
    public interface IDataProviderAsync
    {
        Task<string> getPageHTMLAsync(string url);
    }
}
