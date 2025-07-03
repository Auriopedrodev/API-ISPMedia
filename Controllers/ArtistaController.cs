

namespace ISPMediaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistaController : ControllerBase
{
    private readonly ArtistaService _artistaService;

    /*public ArtistaController(ArtistaService artistaService)
    {
        _artistaService = artistaService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Listar todos os artistas",
        Description = "Obtém uma lista de todos os artistas registados na aplicação."
    )]
    public async Task<IActionResult> ListarTodos()
    {
        var lista = await _artistaService.ListarArtistasTodosAsync();
        return Ok(lista);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Listar artista por ID",
        Description = "Obtém os detalhes de um artista específico pelo seu ID."
    )]
    public async Task<IActionResult> ListarPorId(Guid id)
    {
        var artista = await _artistaService.ListarArtistasPorIdAsync(id);
        if (artista == null)
            return NotFound(new { mensagem = "Artista não encontrado." });
        return Ok(artista);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Criar um novo artista",
        Description = "Regista um novo artista na aplicação com os dados fornecidos."
    )]
    public async Task<IActionResult> Criar([FromBody] GeneroAddDTO dto)
    {
        var novoArtista = await _artistaService.CriarArtistaAsync(dto);
        if (novoArtista == null)
            return BadRequest(new { mensagem = "Já existe um artista com o mesmo nome." });
        return CreatedAtAction(nameof(ListarPorId), new { id = novoArtista.Id }, novoArtista);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Atualizar artista",
        Description = "Atualiza os dados de um artista existente pelo seu ID."
    )]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] GeneroUpdateDTO dto)
    {
        var atualizado = await _artistaService.AtualizarArtistaAsync(id, dto);
        if (!atualizado)
            return NotFound(new { mensagem = "Artista não encontrado." });
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Eliminar artista",
        Description = "Remove um artista da aplicação pelo seu ID."
    )]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        var eliminado = await _artistaService.EliminarArtistaAsync(id);
        if (!eliminado)
            return NotFound(new { mensagem = "Artista não encontrado." });
        return NoContent();
    }*/

}
