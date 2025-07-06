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

    public async Task<MusicaAddDTO> CriarMusicaAsync(MusicaAddDTO dtomusica, IFormFile media)
    {
        var genero = new Genero { Nome = dtomusica.TipoGenero.Nome };
        var generoNovo = new Genero();
        // Verificar se já existe o email ou username
        if (await _ispmediacontext.Musicas.AnyAsync(u => u.TipoGenero.Nome == dtomusica.TipoGenero.Nome))
        {
            return null;
        }
        else
        {
            var Resultado = await _ispmediacontext.Generos.AddAsync(genero);
            generoNovo = Resultado.Entity;
        }

        // ADICIONAR LANCAMENTO
        /*var lancamento = await _ispmediacontext.Lancamento.FirstOrDefaultAsync(p => p.Nome == dtomusica.Titulo); ??
                    await _ispmediacontext.Lancamento.AddAsync(new Lancamento {
                       Titulo= dtomusica.Titulo
                       fichaTecnica= dtomusica.
                       dataLancamento=
                       tipoLancamento=
                       Capa=

    MusicReleaseName = music.MusicRelease.MusicReleaseName,
                        MusicReleaseDescription = music.MusicRelease.MusicReleaseDescription,
                        ReleaseDate = music.MusicRelease.ReleaseDate,
                        ReleaseType = music.MusicRelease.ReleaseType,
                        Cover = Path.Combine(_uploadPath.ImageUploadDir, imageFileName)
                    });*/


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

    //-------------Não esta trazer a lista de compositores e Participacoes!!!--------
    public async Task<List<MusicaGetDTO>> ListarTodasMusicasAsync()
    {
        var musica = await _ispmediacontext.
            Musicas.
            Include(X => X.TipoGenero).
            Include(X => X.Compositores).
            Include(X => X.Autor.Produtora).
            Include(X => X.Autor.Banda).
            Include(X => X.Participacoes).ToListAsync();
        var dto = musica.Adapt<List<MusicaGetDTO>>();
        return dto;
    }

    //-------------Não esta trazer a lista de compositores e Participacoes!!!--------
    public async Task<Musica?> ListarMusicaPorIdAsync(Guid id)
    {
        return await _ispmediacontext.Musicas
            .Include(x => x.TipoGenero)
            .Include(x => x.Compositores)
            .Include(x => x.Autor.Banda)
            .Include(x => x.Autor.Produtora)
            .Include(x => x.Participacoes)
            .AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    } 

    //  ELIMINAR MUSICA
    public async Task<bool> EliminarMusicaAsync(Guid id)
    {
        var musica = await _ispmediacontext.Musicas.FindAsync(id);

        if (musica == null)
            return false;

        _ispmediacontext.Musicas.Remove(musica);
        await _ispmediacontext.SaveChangesAsync();

        return true;
    }


    //-------------Não esta alterar lista de compositores e Participacoes!!!--------
   
    public async Task<bool> ActualizarMusicaAsync(Guid id, MusicaAddDTO dtomusica, IFormFile? media)
    {
        var musicaExistente = await _ispmediacontext.Musicas
            .Include(x => x.Compositores)
            .Include(x => x.Participacoes)
            .Include(x => x.Autor)
            .Include(x => x.TipoGenero)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (musicaExistente == null)
            return false;

        // Atualizar campos simples
        musicaExistente.Titulo = dtomusica.Titulo;
        musicaExistente.Descricao = dtomusica.Descricao;
        musicaExistente.Letra = dtomusica.Letra;

        // Atualizar mídia (se fornecida)
        if (media != null && media.Length > 0)
        {
            var pasta = Path.Combine("wwwroot", "Media", "Audios");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var caminhoCompleto = Path.Combine(pasta, media.FileName);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }

            musicaExistente.CaminhoMedia = caminhoCompleto.Replace("\\", "/");
            musicaExistente.Tamanho = media.Length;
        }

        // Atualizar gênero
        var genero = await _ispmediacontext.Generos.FirstOrDefaultAsync(g => g.Nome == dtomusica.TipoGenero.Nome);
        if (genero == null)
        {
            genero = (await _ispmediacontext.Generos.AddAsync(new Genero { Nome = dtomusica.TipoGenero.Nome })).Entity;
        }
        musicaExistente.TipoGenero = genero;

        // Atualizar autor
        var produtora = await _ispmediacontext.Produtoras.FirstOrDefaultAsync(p => p.Nome == dtomusica.Autor.Produtora.Nome)
                        ?? (await _ispmediacontext.Produtoras.AddAsync(new Produtora { Nome = dtomusica.Autor.Produtora.Nome })).Entity;

        var banda = await _ispmediacontext.Bandas.FirstOrDefaultAsync(b => b.Nome == dtomusica.Autor.Banda.Nome)
                    ?? (await _ispmediacontext.Bandas.AddAsync(new Banda { Nome = dtomusica.Autor.Banda.Nome })).Entity;

        var autor = await _ispmediacontext.Autores.FirstOrDefaultAsync(a => a.Nome == dtomusica.Autor.Nome);
        if (autor == null)
        {
            autor = (await _ispmediacontext.Autores.AddAsync(new Autor
            {
                Nome = dtomusica.Autor.Nome,
                Produtora = produtora,
                Banda = banda
            })).Entity;
        }
        musicaExistente.Autor = autor;

        await _ispmediacontext.SaveChangesAsync();
        return true;
    }

    }
