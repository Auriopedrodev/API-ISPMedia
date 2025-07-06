namespace ISPMediaAPI.Models;

public class Participacao:Artista
{
    // Relacionamento many-to-many com Media
    public virtual ICollection<Media> Medias { get; set; } = new List<Media>();

}
