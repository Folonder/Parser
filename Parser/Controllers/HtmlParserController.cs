using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parser.Services;

namespace Parser.Controllers
{
    [Route("api/[controller]")]
    public class HtmlParserController: ControllerBase
    {
        private readonly ILogger<HtmlParserController> _logger;

        private readonly IHtmlParserService _htmlParserService;

        public HtmlParserController(ILogger<HtmlParserController> logger, IHtmlParserService htmlParserService)
        {
            _logger = logger;
            _htmlParserService = htmlParserService;
        }

        [HttpGet]
        [Route("title")]
        public async Task<IActionResult> GetTitle([FromQuery(Name = "url")] string url)
        {
            return Ok(await _htmlParserService.GetSiteTitleAsync(url));
        }
    }
}
