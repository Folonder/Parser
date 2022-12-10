using System.ComponentModel.DataAnnotations;

namespace Parser.Infrastructure
{

    public class HttpClientFactoryOptions
    {
        public const string Key = "HttpClientFactory";
        
        [Required]
        public string UserAgent { get; set; }
    }
    
    public class DefaultHtmlProviderOptions
    {

        public const string Key = "DefaultHtmlProvider";
        
        [Required, Url]
        public string Domain { get; set; }
    }

    public class AliexpressHtmlProviderOptions
    {
        public const string Key = "AliexpressHtmlProvider";
        
        [Required, Url]
        public string Domain { get; set; }
        [Required, Range(0, 10)]
        public uint MaxRedirectionDeep { get; set; }
    }
}