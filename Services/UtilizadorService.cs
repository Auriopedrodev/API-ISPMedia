using Mapster;

namespace ISPMediaAPI.Services;

public class UtilizadorService
{
    private readonly ISPMediaDbContext _ispmediacontext;

    public UtilizadorService(ISPMediaDbContext context)
    {
        _ispmediacontext = context; 
    }

    public async Task<UtilizadorGetDTO?> CriarUtilizadorAsync(UtilizadorAddDTO dto)
    {
        // Verificar se já existe o email ou username
        if (await _ispmediacontext.Utilizadores.AnyAsync(u => u.Email == dto.Email || u.Username == dto.Username))
            return null;

        var utilizador = new Utilizador
        {
            PrimeiroNome = dto.PrimeiroNome,
            UltimoNome = dto.UltimoNome,
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password,
            Tipo = dto.Tipo,
            Estado = true
        };

        _ispmediacontext.Utilizadores.Add(utilizador);
        await _ispmediacontext.SaveChangesAsync();

        var utilizadorCriado = utilizador.Adapt<UtilizadorGetDTO>();

        return utilizadorCriado;
    }

    public async Task<bool> AtualizarUtilizadorAsync(UtilizadorUpdateDTO dto)
    {
        var utilizador = await _ispmediacontext.Utilizadores.FindAsync(dto.Id);
        if (utilizador == null) return false;

        utilizador.PrimeiroNome = dto.PrimeiroNome;
        utilizador.UltimoNome = dto.UltimoNome;
        utilizador.Username = dto.Username;
        utilizador.Email = dto.Email;

        _ispmediacontext.Utilizadores.Update(utilizador);
        await _ispmediacontext.SaveChangesAsync();
        return true;
    }

    // Função de Login
    public async Task<UtilizadorGetDTO?> LoginAsync(string emailOrUsername, string password)
    {
        // Procurar utilizador por email ou username
        var utilizador = await _ispmediacontext.Utilizadores
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                (u.Email == emailOrUsername || u.Username == emailOrUsername) &&
                u.Password == password &&
                u.Estado == true);

        if (utilizador == null)
            return null;

        var dto = utilizador?.Adapt<UtilizadorGetDTO>();

        return dto;
    }

    public async Task<bool> AlterarPalvaraPasseUtilizadorAsync(Guid id, string Password)
    {
        var utilizador = await _ispmediacontext.Utilizadores.FindAsync(id);
        if (utilizador == null) return false;

        utilizador.Password = Password;
        await _ispmediacontext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Utilizador>> ListarTodosUtilizadoresAsync()
    {
        return await _ispmediacontext.Utilizadores.AsNoTracking().ToListAsync();
    }

    public async Task<Utilizador?> ListarUtilizadorPorIdAsync(Guid id)
    {
        return await _ispmediacontext.Utilizadores.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> EliminarUtilizadorAsync(Guid id)
    {
        var utilizador = await _ispmediacontext.Utilizadores.FindAsync(id);
        if (utilizador == null) return false;

        _ispmediacontext.Utilizadores.Remove(utilizador);
        await _ispmediacontext.SaveChangesAsync();
        return true;
    }
}
