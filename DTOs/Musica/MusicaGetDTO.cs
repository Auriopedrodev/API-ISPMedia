namespace ISPMediaAPI.DTOs.Musica;

public class MusicaGetDTO : MediaGetDTO
{
    public Guid Id { get; set; }
    public string? Letra { get; set; }

    public GeneroGetDTO? TipoGenero { get; set; }

    public List<CompositorGetDTO> Compositores { get; set; }
}