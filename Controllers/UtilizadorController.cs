using Swashbuckle.AspNetCore.Annotations;

namespace ISPMediaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilizadorController : ControllerBase
{
    private readonly UtilizadorService _utilizadorService;

    public UtilizadorController(UtilizadorService utilizadorService)
    {
        _utilizadorService = utilizadorService;
    }

    [HttpGet]
    [SwaggerOperation (
        Summary = "Listar todos os utilizadores",
        Description = "Obtém uma lista de todos os utilizadores registados na aplicação."
    )]
    public async Task<IActionResult> ListarTodos()
    {
        var lista = await _utilizadorService.ListarTodosUtilizadoresAsync();
        return Ok(lista);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Listar utilizador por ID",
        Description = "Obtém os detalhes de um utilizador específico pelo seu ID."
    )]
    public async Task<IActionResult> ListarPorId(Guid id)
    {
        var utilizador = await _utilizadorService.ListarUtilizadorPorIdAsync(id);
        if (utilizador == null)
            return NotFound(new { mensagem = "Utilizador não encontrado." });

        return Ok(utilizador);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Criar um novo utilizador",
        Description = "Regista um novo utilizador na aplicação com os dados fornecidos."
    )]
    public async Task<IActionResult> Criar([FromBody] UtilizadorAddDTO dto)
    {
        var novoUtilizador = await _utilizadorService.CriarUtilizadorAsync(dto);
        if (novoUtilizador == null)
            return BadRequest(new { mensagem = "E-mail ou username já em uso." });

        return CreatedAtAction(nameof(ListarPorId), new { id = novoUtilizador.Id }, novoUtilizador);
    }

    [HttpPost("login")]
    [SwaggerOperation(
       Summary = "Login do utilizador",
       Description = "Login de um utilizador existente na aplicação com os dados fornecidos."
    )]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var result = await _utilizadorService.LoginAsync(dto.emailOrUsername, dto.Password);

        if (result == null)
        {
            return NotFound(new { mensagem = "Utilizador não foi encontrado." });
        }

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Atualizar um utilizador",
        Description = "Atualiza os dados de um utilizador existente com base no ID fornecido."
    )]
    public async Task<IActionResult> Atualizar([FromBody] UtilizadorUpdateDTO dto)
    {
        var atualizado = await _utilizadorService.AtualizarUtilizadorAsync(dto);
        if (!atualizado)
            return NotFound(new { mensagem = "Utilizador não encontrado para atualização." });

        return Ok(new { mensagem = "Utilizador atualizado com sucesso." });
    }

    /*[HttpPut("{id:int}")]
    public async Task<IActionResult> AlterarPalavraPasse(int id, [FromBody] UtilizadorUpdateDTO dto)
    {
        var atualizado = await _utilizadorService.AtualizarUtilizadorAsync(id, dto);
        if (!atualizado)
            return NotFound(new { mensagem = "Utilizador não encontrado para atualização." });

        return Ok(new { mensagem = "Utilizador atualizado com sucesso." });
    }*/

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Eliminar um utilizador",
        Description = "Remove um utilizador da aplicação com base no ID fornecido."
    )]
    public async Task<IActionResult> Delete(Guid id)
    {
        var eliminado = await _utilizadorService.EliminarUtilizadorAsync(id);
        if (!eliminado)
            return NotFound(new { mensagem = "Utilizador não encontrado para eliminação." });

        return Ok(new { mensagem = "Utilizador eliminado com sucesso." });
    }
}
