namespace Parser.Infrastructure
{
    
    public class DefaultHtmlProviderOptions
    {
        public const string Key = "DefaultHtmlProvider";
        public string Domain { get; set; }
        public string UserAgent { get; set; }
    }

    public class AliexpressHtmlProviderOptions
    {
        public const string Key = "AliexpressHtmlProvider";
        public string Domain { get; set; }
        public string UserAgent { get; set; }
        public uint MaxRedirectionDeep { get; set; }
    }
}