namespace ISPMediaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MusicaController : ControllerBase
{
    public readonly MusicaService _musicaService;

    public MusicaController(MusicaService musicaService)
    {
        _musicaService = musicaService;
    }

    [HttpGet]
    [SwaggerOperation(
           Summary = "Listar todas as musicas",
           Description = "Obtém uma lista de todas as musicas registados na aplicação."
       )]
    public async Task<IActionResult> ListarTodos()
    {
        var listaMusica = await _musicaService.ListarTodasMusicasAsync();
        return Ok(listaMusica);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Listar musica por ID",
        Description = "Obtém os detalhes de uma musica específico pelo seu ID."
    )]
    public async Task<IActionResult> ListarPorId(Guid id)
    {
        var musica = await _musicaService.ListarMusicaPorIdAsync(id);
        if (musica == null)
            return NotFound(new { mensagem = "Musica não encontrado." });
        return Ok(musica);
    }

    [HttpPost]
    [SwaggerOperation(
    Summary = "Criar uma nova musica",
    Description = "Regista uma nova musica na aplicação. Compositores e participações devem ser separados por vírgula."
    )]
    public async Task<IActionResult> Criar([FromForm] MusicaAddFormDto musicaDto,
                                         IFormFile media)
    {
        try
        {
            var novaMusica = await _musicaService.CriarMusicaComFormDataAsync(musicaDto, media);

            if (novaMusica == null)
                return BadRequest(new { mensagem = "Erro ao criar música." });

            return Ok(novaMusica);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = $"Erro ao processar dados: {ex.Message}" });
        }
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Atualizar musica",
        Description = "Atualiza os dados de uma musica existente pelo seu ID."
    )]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] MusicaAddDTO dtomusica, IFormFile? media)
    {
        var atualizado = await _musicaService.ActualizarMusicaAsync(id, dtomusica, media);
        if (!atualizado)
            return NotFound(new { mensagem = "Musica não encontrada." });
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Eliminar musica",
        Description = "Remove uma musica da aplicação pelo seu ID."
    )]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        var eliminado = await _musicaService.EliminarMusicaAsync(id);
        if (!eliminado)
            return NotFound(new { mensagem = "Musica não encontrada." });
        return NoContent();
    }
}