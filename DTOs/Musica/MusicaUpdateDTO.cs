using ISPMediaAPI.DTOs.CompositorDTO;
using ISPMediaAPI.DTOs.Genero;
using ISPMediaAPI.DTOs.Media;

namespace ISPMediaAPI.DTOs.Musica;

public class VideoUpdateDTO: MediaUpdateDTO 
{
    public Guid Id { get; set; }
    public string? Letra { get; set; }

    [Required]
    public GeneroAddDTO? TipoGenero { get; set; }

    public List<CompositorAddDTO> Compositores { get; set; } = new List<CompositorAddDTO>();
}
