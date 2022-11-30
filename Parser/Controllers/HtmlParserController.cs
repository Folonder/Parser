using System;
using System.Net.Http;
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
            _logger.LogInformation("Requested url: {url}", url);
            try
            {
                var title = await _htmlParserService.GetSiteTitleAsync(url);
                _logger.LogInformation("Success response, title: {title}", title);
                return Ok(title);
            }
            catch (HttpRequestException ex)
            {  
                _logger.LogInformation("Can't connect to the server");
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("No title on the page");
                return NoContent();
            }
        }
    }
}
