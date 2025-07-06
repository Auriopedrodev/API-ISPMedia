using ISPMediaAPI.DTOs.CompositorDTO;

namespace ISPMediaAPI.DTOs.Musica;

public class MusicaAddDTO : MediaAddDTO
{
    public string? Letra { get; set; }

    [Required]
    public GeneroAddDTO? TipoGenero { get; set; }

    public List<CompositorAddDTO> Compositores { get; set; }
}