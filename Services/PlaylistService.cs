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
            var novaMedia = await _ispmediacontext.Medias.AsNoTracking().FirstOrDefaultAsync(a => a.Id == Guid.Parse(mediaId));
            if (novaMedia != null)
            {
                medias.Add(novaMedia);
            }
        }

        var novaPlaylist = new Playlist
        {
            Nome = dtoplaylist.Nome,
            listaMedia = medias
        };

        _ispmediacontext.Playlists.Add(novaPlaylist);
        await _ispmediacontext.SaveChangesAsync();
        var dto = novaPlaylist.Adapt<PlaylistAddDTO>();
        return dto;

    }

    
    public async Task<List<PlaylistGetDTO>> ListarTodaPlaylistAsync()    {
        var musica = await _ispmediacontext.
            Playlists.Include(x => x.listaMedia).ToListAsync();
        var dto = musica.Adapt<List<PlaylistGetDTO>>();
        return dto;
    }
}