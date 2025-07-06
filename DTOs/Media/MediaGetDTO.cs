namespace ISPMediaAPI.DTOs.Media;

public class MediaGetDTO
{
    public Guid Id { get; set; }

    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public AutorGetDTO Autor { get; set; }

    public List<ParticipacaoGetDTO> Participacoes { get; set; }
}