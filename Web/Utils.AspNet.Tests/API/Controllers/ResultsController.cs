using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using LightningArc.Utils.Results;
using LightningArc.Utils.Results.AspNet;
using LightningArc.Utils.Results.AspNet.Extensions;

namespace LightningArc.Utils.AspNet.Tests.API.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("v1/[Controller]")]
    public class ResultsController(ILogger<ResultsController> logger) : Controller
    {
        private readonly ILogger<ResultsController> _logger = logger;

        [HttpGet]
        public EndpointResult<string> GetResult()
        {
            try
            {
                var t = typeof(Success<string>.CreatedSuccess);


                //return Error.Application.Internal("Erro de teste", [ new ("Teste", "Erro de teste") ]);
                //return Error.Database.ConstraintViolation("Erro de teste", [ new ("Teste", "Erro de teste") ]);
                return Error.Custom<Business>().OrderRejected("Erro de teste", [new("Teste", "Erro de teste")]);
                //return Result.Success("Resultado", Success.Created("Criado com sucesso"));

            }
            catch (Exception exception)
            {
                _logger.LogError("{Message}", exception.Message);
                return Error.Application.Internal("Ocorreu um erro");
            }
        }


        [HttpGet("Teste")]
        public EndpointResult<string> GetTestResult()
        {
            try
            {
                return Result.Success("Teste bem sucedido");
            }
            catch (Exception exception)
            {
                _logger.LogError("{Message}", exception.Message);
                return Error.Application.Internal("Ocorreu um erro");
            }
        }

        [HttpGet("Example")]
        public EndpointResult<string> Example()
        {
            return Result.Success("{\"teste\": \"teste\"}").WithContentType("application/json"); //, "Exemplo bem sucedido"
        }
    }
}
