using ISPMediaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandaController : ControllerBase
    {
        public readonly BandaService _bandaService;

        public BandaController(BandaService bandaService)
        {
            _bandaService = bandaService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar todas as bandas",
            Description = "Obtém uma lista de todas as bandas registados na aplicação."
        )]
        public async Task <IActionResult> ListarTodos()
        {
            var lista = await _bandaService.ListarTodosAsync();
            return Ok(lista);
        }


        [HttpGet("{id:guid}")]
            [SwaggerOperation(
            Summary = "Listar bandas por ID",
            Description = "Obtém os detalhes de uma banda específica pelo seu ID."
        )]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var banda = await _bandaService.ListarPorIdAsync(id);
            if (banda == null)
                return NotFound(new { mensagem = "Banda não encontrado." });

            return Ok(banda);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar uma nova banda",
            Description = "Regista um novo utilizador na aplicação com os dados fornecidos."
        )]
        public async Task<IActionResult> Criar([FromBody] BandaAddDTO dto)
        {
            var novaBanda = await _bandaService.CriarBandaAsync(dto);
            if (novaBanda == null)
                return BadRequest(new { mensagem = "Já existe uma banda com o mesmo nome." });

            return CreatedAtAction(nameof(ListarPorId), new { id = novaBanda.Id }, novaBanda);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Banda artista",
            Description = "Atualiza os dados de um artista existente pelo seu ID."
        )]
        public async Task<IActionResult> Atualizar([FromBody] BandaUpdateDTO dto)
        {
            var bandactualizada = await _bandaService.AtualizarBandaAsync(dto);
            if (!bandactualizada)
                return NotFound(new { mensagem = "Banda não encontrada." });
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(
        Summary = "Eliminar uma Banda",
        Description = "Remove uma banda da aplicação com base no ID fornecido."
    )]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bandaEliminado = await _bandaService.EliminarBandaAsync(id);
            if (!bandaEliminado)
                return NotFound(new { mensagem = "Banda não encontrada para eliminação." });

            return Ok(new { mensagem = "Bnada eliminado com sucesso." });
        }





    }

   
}
