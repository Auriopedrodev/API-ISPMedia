namespace ISPMediaAPI.Models;

public class Produtora
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(255)]
    public string Nome { get; set; }

    // Propriedade de navegação one-to-many com Autor
    public virtual ICollection<Autor> Autores { get; set; } = new List<Autor>();
}
