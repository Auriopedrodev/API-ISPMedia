using ISPMediaAPI.DTOs.Musica;

namespace ISPMediaAPI.DTOs.Album;

public class AlbumAddDTO
{

    public string TituloAlbum { get; set; }
    public string Descricao { get; set; }
    public string dataLancamentoAlbum { get; set; }
    public string CapaAlbum { get; set; }
    public int Classificacao { get; set; }
    public List<MusicaAddDTO> Musicas { get; set; } = new List<MusicaAddDTO>();
}
