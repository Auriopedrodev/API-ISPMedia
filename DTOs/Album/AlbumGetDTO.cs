using ISPMediaAPI.DTOs.Musica;

namespace ISPMediaAPI.DTOs.Album;

public class AlbumGetDTO
{
    public Guid Id { get; set; }
    public string TituloAlbum { get; set; }
    public string Descricao { get; set; }
    public string dataLancamentoAlbum { get; set; }
    public string CapaAlbum { get; set; }
    public int Classificacao { get; set; }
    public List<MusicaGetDTO> Musicas { get; set; } = new List<MusicaGetDTO>();
}
