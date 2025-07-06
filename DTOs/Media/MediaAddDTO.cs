using ISPMediaAPI.DTOs.AutorDTO;
using ISPMediaAPI.DTOs.ParticipacaoDTO;

namespace ISPMediaAPI.DTOs.Media;

public class MediaAddDTO
{

    [Required]
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public AutorAddDTO Autor { get; set; }

    public List<ParticipacaoAddDTO> Participacoes { get; set; }
}