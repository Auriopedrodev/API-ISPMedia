namespace ISPMediaAPI.Models;
using System.ComponentModel.DataAnnotations;

public class Genero
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    // Propriedade de navegação one-to-many com Musica
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
}