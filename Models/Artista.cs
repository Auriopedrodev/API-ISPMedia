namespace ISPMediaAPI.Models;

public class Artista
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Nome { get; set; }
  
}
