

using ISPMediaAPI.DTOs.Banda;

namespace ISPMediaAPI.Services;

public class BandaService
{
    public readonly ISPMediaDbContext _ispmediacontext;

    public BandaService (ISPMediaDbContext context)
    {
        _ispmediacontext = context;
    }


    public async Task<BandaGetDTO?> CriarBandaAsync(BandaAddDTO dto)
    {
        // Verificar se já existe uma Banda  com o mesmo nome
        if (await _ispmediacontext.Bandas.AnyAsync(a => a.Nome == dto.Nome))
            return null;

        var banda = new Banda
        {
            Nome = dto.Nome,
            Biografia = dto.Biografia,
            Inicio = dto.Inicio,
            Fim = dto.Fim
        };

        _ispmediacontext.Bandas.Add(banda);
        await _ispmediacontext.SaveChangesAsync();

        var bandaCriada = banda.Adapt<BandaGetDTO>();
        return bandaCriada;
    }

    public async Task<bool> AtualizarBandaAsync(BandaUpdateDTO dto)
    {
        var banda = await _ispmediacontext.Bandas.FindAsync(dto.Id);
        if (banda == null) return false;

        banda.Nome = dto.Nome;
        banda.Biografia = dto.Biografia;
        banda.Inicio = dto.Inicio;
        banda.Fim = dto.Fim;

        _ispmediacontext.Bandas.Update(banda);
        await _ispmediacontext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Banda?>> ListarTodosAsync()
    {
        return await _ispmediacontext.Bandas.AsNoTracking().ToListAsync();
    }

    public async Task<Banda?> ListarPorIdAsync(Guid id)
    {
        return await _ispmediacontext.Bandas.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task <bool> EliminarBandaAsync(Guid id)
    {
        var banda = await _ispmediacontext.Bandas.FindAsync(id);
        if(banda == null) return false;

        _ispmediacontext.Bandas.Remove(banda);
        await _ispmediacontext.SaveChangesAsync();
        return true;
    }




















}
