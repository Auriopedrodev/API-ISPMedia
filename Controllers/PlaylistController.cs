using ISPMediaAPI.DTOs.Musica;
using ISPMediaAPI.DTOs.Playlist;
using ISPMediaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPMediaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaylistController : ControllerBase
{
    private readonly PlaylistService _playlistService;

    public PlaylistController(PlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpGet]
    [SwaggerOperation(
       Summary = "Listar todas as playlist",
       Description = "Obtém uma lista de todas as playlist registados na aplicação."
   )]
    public async Task<IActionResult> ListarTodos()
    {
        var listaMusica = await _playlistService.ListarTodaPlaylistAsync();
        return Ok(listaMusica);
    }

    [HttpPost]
    [SwaggerOperation(
    Summary = "Criar um nova playlist",
    Description = "Regista um novo utilizador na aplicação com os dados fornecidos."
    )]
    public async Task<IActionResult> Criar([FromBody] PlaylistAddDTO dto)
    {
        var novaMusica = await _playlistService.CriarPlaylistAsync(dto);
        if (novaMusica == null)
            return BadRequest(new { mensagem = "E-mail ou username já em uso." });

        return Ok(novaMusica);


    }
}