using System.Net;

namespace Parser.Infrastructure
{
    public class Requester : IDataProvider
    {
        public Requester(){}

        public string getPageHTML(string url)
        {
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadString("https://" + url);
            }
        }
    }
}
