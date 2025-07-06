namespace ISPMediaAPI.Models;

public class Playlist
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(255)]
    public string Nome { get; set; }

    // Relacionamento many-to-many com Media
    public virtual ICollection<Media> Medias { get; set; } = new List<Media>();
}
