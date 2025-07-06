using ISPMediaAPI.DTOs.Musica;
using ISPMediaAPI.DTOs.Video;
using ISPMediaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        public readonly VideoService _videoService;

        public VideoController(VideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Listar todos os videos",
        Description = "Obtém uma lista de todos os videos registados na aplicação."
        )]
        public async Task<IActionResult> ListarTodos()
        {
            var listaVideos = await _videoService.ListarTodosVideosAsync();
            return Ok(listaVideos);
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Listar video por ID",
            Description = "Obtém os detalhes de uma video específico pelo seu ID."
        )]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var video = await _videoService.ListarVideoPorIdAsync(id);
            if (video == null)
                return NotFound(new { mensagem = "Musica não encontrado." });
            return Ok(video);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um nova video",
            Description = "Regista um novo video na aplicação com os dados fornecidos."
        )]
        public async Task<IActionResult> Criar([FromForm] VideoAddDTO dto, IFormFile media)
        {
            var novaVideo = await _videoService.CriarVideoAsync(dto, media);
            if (novaVideo == null)
                return BadRequest(new { mensagem = "E-mail ou username já em uso." });

            return Ok(novaVideo);
        }

        /*[HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Atualizar video",
            Description = "Atualiza os dados de uma video existente pelo seu ID."
        )]
        public async Task<IActionResult> Atualizar(Guid id, MusicaAddDTO dtomusica, IFormFile? media)
        {
            var atualizado = await _videoService.ActualizarVideoAsync(id, dtomusica, media);
            if (!atualizado)
                return NotFound(new { mensagem = "Musica não encontrada." });
            return NoContent();
        }*/

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(
            Summary = "Eliminar video",
            Description = "Remove uma musica da aplicação pelo seu ID."
        )]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await _videoService.EliminarVideoAsync(id);
            if (!eliminado)
                return NotFound(new { mensagem = "Musica não encontrada." });
            return NoContent();
        }


    }
}
