using ISPMediaAPI.Context;
using ISPMediaAPI.DTOs.Lancamento;
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
        // Verificar se já existe o género
        var generoExistente = await _ispmediacontext.Generos.FirstOrDefaultAsync(g => g.Nome == dtomusica.TipoGenero.Nome);
        var generoNovo = generoExistente ?? (await _ispmediacontext.Generos.AddAsync(new Genero { Nome = dtomusica.TipoGenero.Nome })).Entity;

        // ADICIONAR PRODUTORA 
        var produtoraExistente = await _ispmediacontext.Produtoras.FirstOrDefaultAsync(p => p.Nome == dtomusica.Autor.Produtora.Nome);
        var produtoraNova = produtoraExistente ??
            (await _ispmediacontext.Produtoras.AddAsync(new Produtora { Nome = dtomusica.Autor.Produtora.Nome })).Entity;

        // ADICIONAR BANDA
        var bandaExistente = await _ispmediacontext.Bandas.FirstOrDefaultAsync(p => p.Nome == dtomusica.Autor.Banda.Nome);
        var bandaNova = bandaExistente ??
            (await _ispmediacontext.Bandas.AddAsync(new Banda { Nome = dtomusica.Autor.Banda.Nome })).Entity;

        // ADICIONAR AUTOR
        var autorExistente = await _ispmediacontext.Autores.FirstOrDefaultAsync(p => p.Nome == dtomusica.Autor.Nome);
        var autorNovo = autorExistente ??
                        (await _ispmediacontext.Autores.AddAsync(new Autor
                        {
                            Nome = dtomusica.Autor.Nome,
                            Produtora = produtoraNova,
                            Banda = bandaNova
                        })).Entity;

        // SALVAR MEDIA
        string caminhoCompleto = null;
        if (media != null && media.Length > 0)
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

        // CRIAR A MÚSICA
        var novaMusica = new Musica
        {
            Titulo = dtomusica.Titulo,
            Descricao = dtomusica.Descricao,
            CaminhoMedia = caminhoCompleto,
            Tamanho = media?.Length ?? 0,
            TipoMedia = "audio",
            Letra = dtomusica.Letra,
            TipoGenero = generoNovo,
            Autor = autorNovo,
            Compositores = new List<Compositor>(),
            Participacoes = new List<Participacao>()
        };

        // ADICIONAR COMPOSITORES
        foreach (var compositor in dtomusica.Compositores)
        {
            var compositorExistente = await _ispmediacontext.Compositores.FirstOrDefaultAsync(c => c.Nome == compositor.Nome);
            if (compositorExistente == null)
            {
                var compositorNovo = (await _ispmediacontext.Compositores.AddAsync(new Compositor { Nome = compositor.Nome })).Entity;
                novaMusica.Compositores.Add(compositorNovo);
            }
            else
            {
                novaMusica.Compositores.Add(compositorExistente);
            }
        }
        
        // ADICIONAR PARTICIPAÇÕES
        foreach (var participacao in dtomusica.Participacoes)
        {
            var participacaoExistente = await _ispmediacontext.Participacoes.FirstOrDefaultAsync(c => c.Nome == participacao.Nome);
            if (participacaoExistente == null)
            {
                var participacaoNovo = (await _ispmediacontext.Participacoes.AddAsync(new Participacao { Nome = participacao.Nome })).Entity;
                novaMusica.Participacoes.Add(participacaoNovo);
            }
            else
            {
                novaMusica.Participacoes.Add(participacaoExistente);
            }
        }

        _ispmediacontext.Musicas.Add(novaMusica);
        await _ispmediacontext.SaveChangesAsync();
        
        var dto = novaMusica.Adapt<MusicaAddDTO>();
        return dto;
    }

    public async Task<MusicaAddDTO> CriarMusicaComFormDataAsync(
        MusicaAddFormDto musicaDto,
        IFormFile media)
    {
        // Verificar se já existe o género
        var generoExistente = await _ispmediacontext.Generos.FirstOrDefaultAsync(g => g.Nome == musicaDto.GeneroNome);
        var generoNovo = generoExistente ?? (await _ispmediacontext.Generos.AddAsync(new Genero { Nome = musicaDto.GeneroNome })).Entity;

        // ADICIONAR PRODUTORA 
        var produtoraExistente = await _ispmediacontext.Produtoras.FirstOrDefaultAsync(p => p.Nome == musicaDto.ProdutatoraNome);
        var produtoraNova = produtoraExistente ??
            (await _ispmediacontext.Produtoras.AddAsync(new Produtora { Nome = musicaDto.ProdutatoraNome })).Entity;

        // ADICIONAR BANDA
        var bandaExistente = await _ispmediacontext.Bandas.FirstOrDefaultAsync(p => p.Nome == musicaDto.BandaNome);
        var bandaNova = bandaExistente ??
            (await _ispmediacontext.Bandas.AddAsync(new Banda { Nome = musicaDto.BandaNome })).Entity;

        // ADICIONAR AUTOR
        var autorExistente = await _ispmediacontext.Autores.FirstOrDefaultAsync(p => p.Nome == musicaDto.AutorNome);
        var autorNovo = autorExistente ??
                        (await _ispmediacontext.Autores.AddAsync(new Autor
                        {
                            Nome = musicaDto.AutorNome,
                            Produtora = produtoraNova,
                            Banda = bandaNova
                        })).Entity;

        // SALVAR MEDIA
        string caminhoCompleto = null;
        if (media != null && media.Length > 0)
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

        // Verificar se já existe o lançamento
        var lancamentoExistente = await _ispmediacontext.Lancamentos.FirstOrDefaultAsync(l => l.Titulo == musicaDto.Lancamento.Titulo);
        var lancamentoNovo = lancamentoExistente ??
            (await _ispmediacontext.Lancamentos.AddAsync(new Lancamento
            {
                Titulo = musicaDto.Lancamento.Titulo,
                FichaTecnica = musicaDto.Lancamento.FichaTecnica,
                DataLancamento = musicaDto.Lancamento.DataLancamento,
                TipoLancamento = musicaDto.Lancamento.TipoLancamento,
                Capa = caminhoCompleto
            })).Entity;

        // CRIAR A MÚSICA
        var novaMusica = new Musica
        {
            Titulo = musicaDto.Titulo,
            Descricao = musicaDto.Descricao,
            CaminhoMedia = caminhoCompleto,
            Tamanho = media?.Length ?? 0,
            TipoMedia = "Audio",
            Letra = musicaDto.Letra,
            TipoGenero = generoNovo,
            Autor = autorNovo,
            LancamentoId = lancamentoNovo.Id,
            Compositores = new List<Compositor>(),
            Participacoes = new List<Participacao>()
        };

        // ADICIONAR COMPOSITORES (separados por vírgula)
        if (!string.IsNullOrEmpty(musicaDto.Compositores))
        {
            var compositoresArray = musicaDto.Compositores.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var compositorNome in compositoresArray)
            {
                var nomeLimpo = compositorNome.Trim();
                if (!string.IsNullOrEmpty(nomeLimpo))
                {
                    var compositorExistente = await _ispmediacontext.Compositores.FirstOrDefaultAsync(c => c.Nome == nomeLimpo);
                    if (compositorExistente == null)
                    {
                        var compositorNovo = (await _ispmediacontext.Compositores.AddAsync(new Compositor { Nome = nomeLimpo })).Entity;
                        novaMusica.Compositores.Add(compositorNovo);
                    }
                    else
                    {
                        novaMusica.Compositores.Add(compositorExistente);
                    }
                }
            }
        }
        
        // ADICIONAR PARTICIPAÇÕES (separadas por vírgula)
        if (!string.IsNullOrEmpty(musicaDto.Participacoes))
        {
            var participacoesArray = musicaDto.Participacoes.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var participacaoNome in participacoesArray)
            {
                var nomeLimpo = participacaoNome.Trim();
                if (!string.IsNullOrEmpty(nomeLimpo))
                {
                    var participacaoExistente = await _ispmediacontext.Participacoes.FirstOrDefaultAsync(c => c.Nome == nomeLimpo);
                    if (participacaoExistente == null)
                    {
                        var participacaoNovo = (await _ispmediacontext.Participacoes.AddAsync(new Participacao { Nome = nomeLimpo })).Entity;
                        novaMusica.Participacoes.Add(participacaoNovo);
                    }
                    else
                    {
                        novaMusica.Participacoes.Add(participacaoExistente);
                    }
                }
            }
        }

        _ispmediacontext.Musicas.Add(novaMusica);
        await _ispmediacontext.SaveChangesAsync();
        
        var dto = novaMusica.Adapt<MusicaAddDTO>();
        return dto;
    }

    public async Task<List<MusicaGetDTO>> ListarTodasMusicasAsync()
    {
        var musicas = await _ispmediacontext.Musicas
            .Include(x => x.TipoGenero)
            .Include(x => x.Compositores)
            .Include(x => x.Autor)
                .ThenInclude(a => a.Produtora)
            .Include(x => x.Autor)
                .ThenInclude(a => a.Banda)
            .Include(x => x.Participacoes)
            .AsNoTracking()
            .ToListAsync();
            
        var dto = musicas.Adapt<List<MusicaGetDTO>>();
        return dto;
    }

    public async Task<MusicaGetDTO?> ListarMusicaPorIdAsync(Guid id)
    {
        var musica = await _ispmediacontext.Musicas
            .Include(x => x.TipoGenero)
            .Include(x => x.Compositores)
            .Include(x => x.Autor)
                .ThenInclude(a => a.Produtora)
            .Include(x => x.Autor)
                .ThenInclude(a => a.Banda)
            .Include(x => x.Participacoes)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        var dto = musica.Adapt<MusicaGetDTO>();
        return dto;
    } 

    //  ELIMINAR MUSICA
    public async Task<bool> EliminarMusicaAsync(Guid id)
    {

        var musica = await _ispmediacontext.Musicas.FindAsync(id);

        if (musica == null)
            return false;

        if (!string.IsNullOrEmpty(musica.CaminhoMedia))
{
    string fullPath = musica.CaminhoMedia.Replace('/', Path.DirectorySeparatorChar);

    if (File.Exists(fullPath))
    {
        File.Delete(fullPath);
    }
}


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

        // Atualizar compositores
        musicaExistente.Compositores.Clear();
        foreach (var compositor in dtomusica.Compositores)
        {
            var compositorExistente = await _ispmediacontext.Compositores.FirstOrDefaultAsync(c => c.Nome == compositor.Nome);
            if (compositorExistente == null)
            {
                var compositorNovo = (await _ispmediacontext.Compositores.AddAsync(new Compositor { Nome = compositor.Nome })).Entity;
                musicaExistente.Compositores.Add(compositorNovo);
            }
            else
            {
                musicaExistente.Compositores.Add(compositorExistente);
            }
        }

        // Atualizar participações
        musicaExistente.Participacoes.Clear();
        foreach (var participacao in dtomusica.Participacoes)
        {
            var participacaoExistente = await _ispmediacontext.Participacoes.FirstOrDefaultAsync(c => c.Nome == participacao.Nome);
            if (participacaoExistente == null)
            {
                var participacaoNovo = (await _ispmediacontext.Participacoes.AddAsync(new Participacao { Nome = participacao.Nome })).Entity;
                musicaExistente.Participacoes.Add(participacaoNovo);
            }
            else
            {
                musicaExistente.Participacoes.Add(participacaoExistente);
            }
        }

        await _ispmediacontext.SaveChangesAsync();
        return true;
    }


}
