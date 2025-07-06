namespace ISPMediaAPI.Models;

public class Media
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Titulo { get; set; }
    
    [StringLength(1000)]
    public string Descricao { get; set; }

    [Required]
    [StringLength(500)]
    public string CaminhoMedia { get; set; }

    public long Tamanho { get; set; }

    [StringLength(50)]
    public string TipoMedia { get; set; }
    
    // Foreign Key para Autor
    public Guid AutorId { get; set; }
    [ForeignKey(nameof(AutorId))]
    public Autor Autor { get; set; }

    // Relacionamento many-to-many com Participacao
    public virtual ICollection<Participacao> Participacoes { get; set; } = new List<Participacao>();
}