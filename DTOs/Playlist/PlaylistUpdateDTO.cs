using ISPMediaAPI.DTOs.Media;

namespace ISPMediaAPI.DTOs.Playlist;

public class PlaylistUpdateDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public List<MediaUpdateDTO> listaMedia = new List<MediaUpdateDTO>();
}
