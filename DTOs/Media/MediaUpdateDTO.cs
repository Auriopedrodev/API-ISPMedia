using ISPMediaAPI.DTOs.Autor;
using ISPMediaAPI.DTOs.AutorDTO;
using ISPMediaAPI.DTOs.CompositorDTO;
using ISPMediaAPI.DTOs.Genero;
using ISPMediaAPI.DTOs.Participacao;
using ISPMediaAPI.DTOs.ParticipacaoDTO;

namespace ISPMediaAPI.DTOs.Media;

public class MediaUpdateDTO
{
    public Guid Id { get; set; }

    [Required]
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public AutorUpdateDTO Autor { get; set; }

    public List<ParticipacaoUpdateDTO> Participacoes { get; set; } = new List<ParticipacaoUpdateDTO>();
}
