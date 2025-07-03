namespace ISPMediaAPI.Models;

public class Musica : Media
{
    public string? Letra { get; set; }

    [Required]
    public Genero TipoGenero { get; set; }
    public Guid TipoGeneroId { get; set; }
    [ForeignKey(nameof(TipoGeneroId))]

    public List<Compositor> Compositores { get; set; } = new List<Compositor>();

}
