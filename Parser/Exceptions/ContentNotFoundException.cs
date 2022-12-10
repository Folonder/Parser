using System;
using System.Runtime.Serialization;

namespace Parser.Exceptions
{
    [Serializable]
    public class ContentNotFoundException : Exception
    {
        public ContentNotFoundException() : base() { }
        
        public ContentNotFoundException(string message) : base(message) { }
        
        public ContentNotFoundException(string message, Exception inner) : base(message, inner) { }
        
        protected ContentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}