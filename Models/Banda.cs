namespace ISPMediaAPI.Models;

public class Banda
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Nome { get; set; } 
    public string? Biografia { get; set; }
    public DateOnly Inicio { get; set; }
    public DateOnly? Fim { get; set; }

    /*[ForeignKey("Utilizador")]
    public int? UtilizadorId { get; set; }
    public virtual Utilizador? Usuario { get; set; }*/


}
