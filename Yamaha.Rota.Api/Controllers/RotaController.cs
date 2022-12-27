using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Yamaha.Rota.Domain.Dominio.Rota.Arguments;
using Yamaha.Rota.Domain.Dominio.Rota.Interfaces;

namespace Yamaha.Rota.Api.Controllers
{

    [Route("api/[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly IRotaService _service;
        public RotaController(IRotaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("melhorRota")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterMelhorRotaAsync(RotaRequest request)
        {
            var retorno = await _service.ObterMelhorRotaAsync(request);

            return retorno.Any() ? Ok(retorno) : (IActionResult)BadRequest("Consulta não realizada");
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UploadRotaAsync(string caminhoArquivo)
        {
            var retorno = await _service.SalvarRotaAsync(caminhoArquivo);

            return retorno.Any() ? Ok(retorno) : (IActionResult)BadRequest("Upload não realizado");
        }
    }
}
