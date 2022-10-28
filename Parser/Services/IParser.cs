using System.Threading.Tasks;

namespace Parser.Services
{
    public interface IParser
    {
        public Task<string> getSiteTitleAsync(string URL);
    }
}
