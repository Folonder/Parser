using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parser.Services;

namespace Parser.Controllers
{
    [Route("api/[controller]")]
    public class HtmlParserController: ControllerBase
    {
        private readonly ILogger<HtmlParserController> _logger;

        private readonly IParser _parser;

        public HtmlParserController(ILogger<HtmlParserController> logger, IParser parser)
        {
            _logger = logger;
            _parser = parser;
        }

        [HttpGet]
        [Route("title")]
        public IActionResult GetTitle([FromQuery(Name = "url")] string url)
        {
            return Ok(_parser.GetSiteTitleAsync(url).Result);
        }
    }
}
