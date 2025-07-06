using ISPMediaAPI.DTOs.CompositorDTO;
using ISPMediaAPI.DTOs.Genero;
using ISPMediaAPI.DTOs.Media;

namespace ISPMediaAPI.DTOs.Musica;

public class MusicaUpdateDTO: MediaUpdateDTO 
{
    public Guid Id { get; set; }
    public string? Letra { get; set; }

    [Required]
    public GeneroUpdateDTO? TipoGenero { get; set; }

    public List<CompositorUpdateDTO> Compositores { get; set; } = new List<CompositorUpdateDTO>();
}
