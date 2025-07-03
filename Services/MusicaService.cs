using ISPMediaAPI.Context;
using ISPMediaAPI.DTOs.Musica;
using ISPMediaAPI.Models;
using Microsoft.Identity.Client;

namespace ISPMediaAPI.Services;

public class MusicaService
{
    public readonly ISPMediaDbContext _ispmediacontext;

    public MusicaService(ISPMediaDbContext ispmediacontext)
    {
        _ispmediacontext = ispmediacontext;
    }

    public async Task<MusicaAddDTO> AddMusicaAsync(MusicaAddDTO dtomusica, IFormFile media)
    {
        var genero = new Genero { Nome = dtomusica.TipoGenero.Nome };
        var generoNovo = new Genero();
        // Verificar se já existe o email ou username
        if (await _ispmediacontext.Musicas.AnyAsync(u => u.TipoGenero.Nome == dtomusica.TipoGenero.Nome )){
            return null;
        }else{
            var Resultado = await _ispmediacontext.Generos.AddAsync(genero);
            generoNovo = Resultado.Entity;
        }
     
        // ADICIONAR PRODUTORA 
        var produtoraExistente = await _ispmediacontext.Produtoras.FirstOrDefaultAsync(p => p.Nome == dtomusica.Autor.Produtora.Nome);
        var produtoraNova = produtoraExistente ??
            (await _ispmediacontext.Produtoras.AddAsync(new Produtora { Nome = dtomusica.Autor.Produtora.Nome })).Entity;

        // ADICIONAR BANDA
        var bandaExistente = await _ispmediacontext.Bandas.FirstOrDefaultAsync(p => p.Nome == dtomusica.Autor.Banda.Nome);
        var bandaNova = bandaExistente ??
            (await _ispmediacontext.Bandas.AddAsync(new Banda { Nome = dtomusica.Autor.Banda.Nome })).Entity;

        // ADICIONAR 
        var autorExistente = await _ispmediacontext.Autores.FirstOrDefaultAsync(p => p.Nome == dtomusica.Autor.Nome);
        var autorNovo = autorExistente ?? 
                        (await _ispmediacontext.Autores.AddAsync(new Autor
                             {
                                 Nome = dtomusica.Autor.Nome,
                                 Produtora = produtoraNova,
                                 Banda = bandaNova
                        })).Entity;

        // ADICIONAR LISTA DE COMPUSITORES
        var listaCompositores = new List<Compositor>();

        foreach (var compositor in dtomusica.Compositores)
        {
            var compositorExistente = await _ispmediacontext.Compositores.FirstOrDefaultAsync(c => c.Nome == compositor.Nome);
            if (compositorExistente == null)
            {
                var compositorNovo = (await _ispmediacontext.Compositores.AddAsync(new Compositor { Nome = compositor.Nome })).Entity;
                listaCompositores.Add(compositorNovo);
            }
            else
            {
                listaCompositores.Add(compositorExistente);
            }
            
        }

        // ADICIONAR LISTA DE Participacoes
        var listaParticipacoes = new List<Participacao>();

        foreach (var participacao in dtomusica.Participacoes)
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
        if (media!=null && media.Length>0)
        {
            var Pasta = Path.Combine("wwwroot", "Media", "Audios");
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

        var novaMusica = new Musica
        {
            Titulo = dtomusica.Titulo,
            Descricao = dtomusica.Descricao,
            CaminhoMedia = caminhoCompleto,
            Tamanho = media.Length,
            TipoMedia = "audio",
            Letra = dtomusica.Letra,
            TipoGenero = generoNovo,
            Autor = autorNovo,
            Compositores = listaCompositores,
            Participacoes = listaParticipacoes,
        };

        _ispmediacontext.Musicas.Add(novaMusica);
        await _ispmediacontext.SaveChangesAsync();
        var dto = novaMusica.Adapt<MusicaAddDTO>();
        return dto;
    }

}
