using ISPMediaAPI.DTOs.Media;

namespace ISPMediaAPI.DTOs.Playlist;

public class PlaylistGetDTO
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public List<MediaGetDTO> listaMedia { get; set; }
}
