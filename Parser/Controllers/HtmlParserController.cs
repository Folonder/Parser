using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parser.Services;

namespace Parser.Controllers
{
    [Route("api/[controller]")]
    public class HtmlParserController: ControllerBase
    {
        private readonly ILogger<HtmlParserController> _logger;

        private readonly IParserService _parserService;

        public HtmlParserController(ILogger<HtmlParserController> logger, IParserService parserService)
        {
            _logger = logger;
            _parserService = parserService;
        }

        [HttpGet]
        [Route("title")]
        public IActionResult GetTitle([FromQuery(Name = "url")] string url)
        {
            return Ok(_parserService.GetSiteTitleAsync(url).Result);
        }
    }
}
