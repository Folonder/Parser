using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Parser.Services;

namespace Parser.Controllers
{
    [ApiController]
    public class TitleParserController
    {
        private readonly ILogger<TitleParserController> _logger;

        private readonly IParser _parser;

        public TitleParserController(ILogger<TitleParserController> logger, IParser parser)
        {
            _logger = logger;
            _parser = parser;
        }

        [HttpGet]
        [Route("title/{url}")]
        public string GetTitle(string url)
        {
            return _parser.getSiteTitle(url);
        }
    }
}
