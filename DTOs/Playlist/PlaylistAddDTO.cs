using ISPMediaAPI.DTOs.Media;

namespace ISPMediaAPI.DTOs.Playlist;

public class PlaylistAddDTO
{

    public string Nome { get; set; }

    public List<string> listaMedia { get; set; }

}
