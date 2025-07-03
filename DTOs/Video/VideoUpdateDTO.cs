using ISPMediaAPI.DTOs.CompositorDTO;
using ISPMediaAPI.DTOs.Genero;
using ISPMediaAPI.DTOs.Media;

namespace ISPMediaAPI.DTOs.Video;

public class VideoUpdateDTO: MediaUpdateDTO 
{
    public string? Descricao { get; set; }
}
