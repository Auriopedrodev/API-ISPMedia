namespace ISPMediaAPI.Models;

public class Produtora
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Nome { get; set; }

    // Propriedade de navegação
    //public virtual ICollection<Artista> Autores { get; set; } = new List<Artista>();
}
