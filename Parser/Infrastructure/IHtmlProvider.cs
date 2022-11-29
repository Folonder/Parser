using System.Threading.Tasks;

namespace Parser.Infrastructure
{
    public interface IHtmlProvider
    {
        Task<string> GetPageHtmlAsync(string url);
        
        string Domain { get; }
    }
}
