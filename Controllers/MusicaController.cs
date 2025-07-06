using ISPMediaAPI.DTOs.Genero;
using ISPMediaAPI.DTOs.Musica;
using ISPMediaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    Summary = "Criar um nova musica",
    Description = "Regista um novo utilizador na aplicação com os dados fornecidos."
    )]
    public async Task<IActionResult> Criar([FromForm] MusicaAddDTO dto, IFormFile media)
    {
        var novaMusica = await _musicaService.CriarMusicaAsync(dto, media);
        if (novaMusica == null)
            return BadRequest(new { mensagem = "E-mail ou username já em uso." });

        return Ok (novaMusica);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Atualizar musica",
        Description = "Atualiza os dados de uma musica existente pelo seu ID."
    )]
    public async Task<IActionResult> Atualizar(Guid id, MusicaAddDTO dtomusica, IFormFile? media)
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