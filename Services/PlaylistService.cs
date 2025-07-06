using ISPMediaAPI.DTOs.Musica;
using ISPMediaAPI.DTOs.Playlist;

namespace ISPMediaAPI.Services;

public class PlaylistService
{
    public readonly ISPMediaDbContext _ispmediacontext;

    public PlaylistService (ISPMediaDbContext ispmediacontext)
    {
        _ispmediacontext = ispmediacontext;
    }

    public async Task<PlaylistAddDTO> CriarPlaylistAsync(PlaylistAddDTO dtoplaylist)
    {
        var medias = new List<Media>();

        foreach (var mediaId in dtoplaylist.listaMedia)
        {
            var mediaExistente = await _ispmediacontext.Medias
                .FirstOrDefaultAsync(a => a.Id == Guid.Parse(mediaId));

            if (mediaExistente != null)
            {
                medias.Add(mediaExistente);
            }
        }

        var novaPlaylist = new Playlist
        {
            Nome = dtoplaylist.Nome,
            Medias = medias
        };

        // Evita tentativa de re-inserção de Medias
        foreach (var media in medias)
        {
            _ispmediacontext.Attach(media);
        }

        _ispmediacontext.Playlists.Add(novaPlaylist);
        await _ispmediacontext.SaveChangesAsync();

        var dto = novaPlaylist.Adapt<PlaylistAddDTO>();
        return dto;
    }



    public async Task<List<PlaylistGetDTO>> ListarTodaPlaylistAsync()    
    {
        var playlists = await _ispmediacontext.Playlists
            .Include(x => x.Medias)
                .ThenInclude(x => x.Autor)
            .Include(x => x.Medias)
                .ThenInclude(x => x.Participacoes)
            .ToListAsync();

        var dto = playlists.Adapt<List<PlaylistGetDTO>>();
        return dto;
    }
}