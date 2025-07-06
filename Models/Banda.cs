namespace ISPMediaAPI.Models;

public class Banda
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(255)]
    public string Nome { get; set; } = string.Empty;
    [StringLength(2000)]
    public string? Biografia { get; set; }
    [Required]
    public DateOnly Inicio { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly? Fim { get; set; }

    /*[ForeignKey("Utilizador")]
    public int? UtilizadorId { get; set; }
    public virtual Utilizador? Usuario { get; set; }*/
}