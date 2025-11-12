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
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("validarCpfOuCnpj/{cpfCnpj}")]
        public ActionResult<StatusResult> ValidarCpfOuCnpj(string cpfCnpj)
        {
            try
            {
                var isValid = DocumentoUtils.ValidarCpfOuCnpj(cpfCnpj);
                _logger.LogInformation("validarCpfOuCnpj: {@cpfCnpj}={@isValid}", cpfCnpj, isValid);
                return new StatusResult
                {
                    Sucesso = isValid,
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
