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

    [HttpPost]
    [SwaggerOperation(
    Summary = "Criar um nova musica",
    Description = "Regista um novo utilizador na aplicação com os dados fornecidos."
    )]
    public async Task<IActionResult> Criar([FromForm] MusicaAddDTO dto, IFormFile media)
    {
        var novaMusica = await _musicaService.AddMusicaAsync(dto, media);
        if (novaMusica == null)
            return BadRequest(new { mensagem = "E-mail ou username já em uso." });

        return Ok (novaMusica);
    }


}