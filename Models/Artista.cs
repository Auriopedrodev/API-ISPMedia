namespace ISPMediaAPI.Models;

public class Artista
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(255)]
    public string Nome { get; set; } = string.Empty;
}