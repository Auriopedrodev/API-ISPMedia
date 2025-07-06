namespace ISPMediaAPI.Models;

public class Compositor: Artista
{
    // Relacionamento many-to-many com Musica
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
}
