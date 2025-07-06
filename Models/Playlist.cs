namespace ISPMediaAPI.Models;

public class Playlist
{
    public Guid Id { get; set; }

    [Required]
    public string Nome { get; set; }

    public List<Media> listaMedia = new List<Media>();

}
