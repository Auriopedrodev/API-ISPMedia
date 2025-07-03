namespace ISPMediaAPI.Services;

public class ArtistaService
{
    /*private readonly ISPMediaDbContext _ispmediacontext;

    public ArtistaService(ISPMediaDbContext context)
    {
        _ispmediacontext = context;
    }

    public async Task<ArtistaGetDTO?> CriarArtistaAsync(ArtistaAddDTO dto)
    {
        // Verificar se já existe um artista com o mesmo nome
        if (await _ispmediacontext.Artistas.AnyAsync(a => a.Nome == dto.Nome))
            return null;

        var artista = new Artista
        {
            Nome = dto.Nome,
            ProdutoraId = dto.ProdutoraId,
            BandaId = dto.BandaId
        };

        _ispmediacontext.Artistas.Add(artista);
        await _ispmediacontext.SaveChangesAsync ();

        var artistaCriado = artista.Adapt<ArtistaGetDTO>();
        return artistaCriado;
    }

    public async Task<bool> AtualizarArtistaAsync(Guid id, ArtistaUpdateDTO dto)
    {
        var artista = await _ispmediacontext.Artistas
            .FirstOrDefaultAsync(x=>x.Id == id);
        if (artista == null) return false;

        artista.Nome = dto.Nome;

        _ispmediacontext.Artistas.Update(artista);
        await _ispmediacontext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Artista>> ListarArtistasTodosAsync()
    {
        return await _ispmediacontext.Artistas
             .Include(x => x.Banda)
            .Include(x => x.Produtora)
            .AsNoTracking().ToListAsync();
    }

    public async Task<Artista?> ListarArtistasPorIdAsync(Guid id)
    {
        return await _ispmediacontext.Artistas
             .Include(x => x.Banda)
            .Include(x => x.Produtora)
            .AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> EliminarArtistaAsync(Guid id)
    {
        var artista = await _ispmediacontext.Artistas.FindAsync(id);
        if (artista == null) return false;

        _ispmediacontext.Artistas.Remove(artista);
        await _ispmediacontext.SaveChangesAsync();
        return true;
    }*/
}
