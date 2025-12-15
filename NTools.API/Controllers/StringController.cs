using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTools.Domain.Utils;
using System;

namespace BazzucaMedia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StringController : ControllerBase
    {
        private readonly ILogger<StringController> _logger;

        public StringController(ILogger<StringController> logger)
        {
            _logger = logger;
        }

        [HttpGet("generateSlug/{name}")]
        public ActionResult<string> GenerateSlug(string name)
        {
            try
            {
                var slug = SlugHelper.GenerateSlug(name);
                _logger.LogInformation("Generate Slug '{Slug}' from string '{Input}'", slug, name);
                return Ok(slug);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating slug for input '{Input}'", name);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("onlyNumbers/{input}")]
        public ActionResult<string> OnlyNumbers(string input)
        {
            try
            {
                var onlyNumber = StringUtils.OnlyNumbers(input);
                _logger.LogInformation("Extract only numbers '{OnlyNumber}' from '{Input}'", onlyNumber, input);
                return Ok(onlyNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while extracting only numbers from input '{Input}'", input);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("generateShortUniqueString")]
        public ActionResult<string> GenerateShortUniqueString()
        {
            try
            {
                var uniqueStr = StringUtils.GenerateShortUniqueString();
                _logger.LogInformation("Generate short unique string: '{UniqueStr}'", uniqueStr);
                return Ok(uniqueStr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating a short unique string");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
