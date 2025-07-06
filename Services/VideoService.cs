using ISPMediaAPI.DTOs.Musica;
using ISPMediaAPI.DTOs.Video;
using ISPMediaAPI.Models;

namespace ISPMediaAPI.Services;

public class VideoService
{
    public readonly ISPMediaDbContext _ispmediacontext;

    public VideoService (ISPMediaDbContext ispmediacontext)
    {
        _ispmediacontext = ispmediacontext;
    }

    public async Task<VideoAddDTO> CriarVideoAsync(VideoAddFormDto dtovideo, IFormFile media)
    {
        // ADICIONAR PRODUTORA 
        var produtoraExistente = await _ispmediacontext.Produtoras.FirstOrDefaultAsync(p => p.Nome == dtovideo.ProdutatoraNome);
        var produtoraNova = produtoraExistente ??
            (await _ispmediacontext.Produtoras.AddAsync(new Produtora { Nome = dtovideo.ProdutatoraNome })).Entity;

        // ADICIONAR BANDA
        var bandaExistente = await _ispmediacontext.Bandas.FirstOrDefaultAsync(p => p.Nome == dtovideo.BandaNome);
        var bandaNova = bandaExistente ??
            (await _ispmediacontext.Bandas.AddAsync(new Banda { Nome = dtovideo.BandaNome })).Entity;

        // ADICIONAR 
        var autorExistente = await _ispmediacontext.Autores.FirstOrDefaultAsync(p => p.Nome == dtovideo.AutorNome);
        var autorNovo = autorExistente ??
                        (await _ispmediacontext.Autores.AddAsync(new Autor
                        {
                            Nome = dtovideo.AutorNome,
                            Produtora = produtoraNova,
                            Banda = bandaNova
                        })).Entity;

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
            TipoMedia = "Video",
            Autor = autorNovo,
            Participacoes = new List<Participacao>()
        };

        // ADICIONAR PARTICIPAÇÕES (separadas por vírgula)
        if (!string.IsNullOrEmpty(dtovideo.Participacoes))
        {
            var participacoesArray = dtovideo.Participacoes.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var participacaoNome in participacoesArray)
            {
                var nomeLimpo = participacaoNome.Trim();
                if (!string.IsNullOrEmpty(nomeLimpo))
                {
                    var participacaoExistente = await _ispmediacontext.Participacoes.FirstOrDefaultAsync(c => c.Nome == nomeLimpo);
                    if (participacaoExistente == null)
                    {
                        var participacaoNovo = (await _ispmediacontext.Participacoes.AddAsync(new Participacao { Nome = nomeLimpo })).Entity;
                        novoVideo.Participacoes.Add(participacaoNovo);
                    }
                    else
                    {
                        novoVideo.Participacoes.Add(participacaoExistente);
                    }
                }
            }
        }

        _ispmediacontext.Videos.Add(novoVideo);
        await _ispmediacontext.SaveChangesAsync();

        var dto = novoVideo.Adapt<VideoAddDTO>();
        return dto;
    }

    
    public async Task<List<VideoGetDTO>> ListarTodosVideosAsync()
    {
        var video = await _ispmediacontext.Videos.
            Include(X => X.Autor.Produtora).
            Include(X => X.Autor.Banda).
            Include(X => X.Participacoes).ToListAsync();

        var dto = video.Adapt<List<VideoGetDTO>>();
        return dto;
    }

    public async Task<VideoGetDTO> ListarVideoPorIdAsync(Guid id)
    {
        var video = await _ispmediacontext.Videos.
            Include(X => X.Autor.Produtora).
            Include(X => X.Autor.Banda).
            Include(X => X.Participacoes)
            .AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        var dto = video.Adapt<VideoGetDTO>();
        return dto;
    }

    public async Task<bool> EliminarVideoAsync(Guid id)
    {
        var video = await _ispmediacontext.Videos.FindAsync(id);

        if (video == null)
            return false;

        if (!string.IsNullOrEmpty(video.CaminhoMedia))
        {
            string fullPath = video.CaminhoMedia.Replace('/', Path.DirectorySeparatorChar);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        _ispmediacontext.Videos.Remove(video);
        await _ispmediacontext.SaveChangesAsync();

        return true;
    }

}