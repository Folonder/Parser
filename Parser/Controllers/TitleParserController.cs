using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parser.Services;

namespace Parser.Controllers
{
    [ApiController]
    public class TitleParserController: ControllerBase
    {
        private readonly ILogger<TitleParserController> _logger;

        private readonly IParser _parser;

        public TitleParserController(ILogger<TitleParserController> logger, IParser parser)
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
