using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTools.Domain.Interfaces.Services;
using NTools.DTO.Domain;
using NTools.DTO.MailerSend;
using System;
using System.Threading.Tasks;

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
        public ActionResult<StringResult> GenerateSlug(string name)
        {
            try
            {
                var slug = SlugHelper.GenerateSlug(name);
                _logger.LogInformation("Generate Slug '{0}' from string '{1}'", slug, name);
                return new StringResult
                {
                    Sucesso = true,
                    Value = slug
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("onlyNumbers/{input}")]
        public ActionResult<StringResult> OnlyNumbers(string input)
        {
            try
            {
                var onlyNumber = StringUtils.OnlyNumbers(input);
                _logger.LogInformation("Extract only numbers `{0}` from {1}", onlyNumber, input);
                return new StringResult
                {
                    Sucesso = true,
                    Value = StringUtils.OnlyNumbers(input)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("generateShortUniqueString")]
        public ActionResult<StringResult> GenerateShortUniqueString()
        {
            try
            {
                var uniqueStr = StringUtils.GenerateShortUniqueString();
                _logger.LogInformation("Generate short unique string: `{0}`", uniqueStr);
                return new StringResult
                {
                    Sucesso = true,
                    Value = StringUtils.GenerateShortUniqueString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
