using ISPMediaAPI.DTOs.CompositorDTO;
using ISPMediaAPI.DTOs.Genero;
using ISPMediaAPI.DTOs.Media;

namespace ISPMediaAPI.DTOs.Video;

public class VideoGetDTO : MediaGetDTO
{
    public string? Descricao { get; set; }
}
