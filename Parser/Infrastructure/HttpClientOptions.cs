namespace Parser.Infrastructure
{

    public class HttpClientFactoryOptions
    {
        public const string Key = "HttpClientFactory";
        public string UserAgent { get; set; }
    }
    
    public class DefaultHtmlProviderOptions
    {
        public const string Key = "DefaultHtmlProvider";
        public string Domain { get; set; }
    }

    public class AliexpressHtmlProviderOptions
    {
        public const string Key = "AliexpressHtmlProvider";
        public string Domain { get; set; }
        public uint MaxRedirectionDeep { get; set; }
    }
}