using ISPMediaAPI.DTOs.Musica;
using ISPMediaAPI.DTOs.Video;

namespace ISPMediaAPI.Services;

public class VideoService
{
    public readonly ISPMediaDbContext _ispmediacontext;

    public VideoService (ISPMediaDbContext ispmediacontext)
    {
        _ispmediacontext = ispmediacontext;
    }

    public async Task<VideoAddDTO> CriarVideoAsync(VideoAddDTO dtovideo, IFormFile media)
    {
        // ADICIONAR PRODUTORA 
        var produtoraExistente = await _ispmediacontext.Produtoras.FirstOrDefaultAsync(p => p.Nome == dtovideo.Autor.Produtora.Nome);
        var produtoraNova = produtoraExistente ??
            (await _ispmediacontext.Produtoras.AddAsync(new Produtora { Nome = dtovideo.Autor.Produtora.Nome })).Entity;

        // ADICIONAR BANDA
        var bandaExistente = await _ispmediacontext.Bandas.FirstOrDefaultAsync(p => p.Nome == dtovideo.Autor.Banda.Nome);
        var bandaNova = bandaExistente ??
            (await _ispmediacontext.Bandas.AddAsync(new Banda { Nome = dtovideo.Autor.Banda.Nome })).Entity;

        // ADICIONAR 
        var autorExistente = await _ispmediacontext.Autores.FirstOrDefaultAsync(p => p.Nome == dtovideo.Autor.Nome);
        var autorNovo = autorExistente ??
                        (await _ispmediacontext.Autores.AddAsync(new Autor
                        {
                            Nome = dtovideo.Autor.Nome,
                            Produtora = produtoraNova,
                            Banda = bandaNova
                        })).Entity;

        // ADICIONAR LISTA DE Participacoes
        var listaParticipacoes = new List<Participacao>();

        foreach (var participacao in dtovideo.Participacoes)
        {
            var participacaoExistente = await _ispmediacontext.Participacoes.FirstOrDefaultAsync(c => c.Nome == participacao.Nome);
            if (participacaoExistente == null)
            {
                var participacaoNovo = (await _ispmediacontext.Participacoes.AddAsync(new Participacao { Nome = participacao.Nome })).Entity;
                listaParticipacoes.Add(participacaoNovo);
            }
            else
            {
                listaParticipacoes.Add(participacaoExistente);
            }

        }

        // SALVAR MEDIA
        string caminhoCompleto = null;
        if (media != null && media.Length > 0)
        {
            var Pasta = Path.Combine("wwwroot", "Media", "Videos");
            if (!Directory.Exists(Pasta))
            {
                Directory.CreateDirectory(Pasta);
            }
            caminhoCompleto = Path.Combine(Pasta, media.FileName);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
            caminhoCompleto = caminhoCompleto.Replace("\\", "/"); // Normalizar o caminho para URL            
        }

        var novoVideo = new Video
        {
            Titulo = dtovideo.Titulo,
            Descricao = dtovideo.Descricao,
            CaminhoMedia = caminhoCompleto,
            Tamanho = media.Length,
            TipoMedia = "video",
            Autor = autorNovo,
            Participacoes = listaParticipacoes,
        };

        _ispmediacontext.Videos.Add(novoVideo);
        await _ispmediacontext.SaveChangesAsync();
        var dto = novoVideo.Adapt<VideoAddDTO>();
        return dto;

    }

    
    public async Task<List<Video>> ListarTodosVideosAsync()
    {
        return await _ispmediacontext.Videos.
            Include(X => X.Autor.Produtora).
            Include(X => X.Autor.Banda).
            Include(X => X.Participacoes).ToListAsync();
    }

    public async Task<Video?> ListarVideoPorIdAsync(Guid id)
    {
        return await _ispmediacontext.Videos.
            Include(X => X.Autor.Produtora).
            Include(X => X.Autor.Banda).
            Include(X => X.Participacoes)
            .AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> EliminarVideoAsync(Guid id)
    {
        var video = await _ispmediacontext.Videos.FindAsync(id);

        if (video == null)
            return false;

        _ispmediacontext.Videos.Remove(video);
        await _ispmediacontext.SaveChangesAsync();

        return true;
    }

}