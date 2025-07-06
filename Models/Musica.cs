namespace ISPMediaAPI.Models;

public class Musica : Media
{
    [StringLength(5000)]
    public string? Letra { get; set; }

    // Foreign Key para Genero
    public Guid TipoGeneroId { get; set; }
    [ForeignKey(nameof(TipoGeneroId))]
    public virtual Genero TipoGenero { get; set; }

    // Foreign Key opcional para Lancamento
    public Guid? LancamentoId { get; set; }
    [ForeignKey(nameof(LancamentoId))]
    public virtual Lancamento? Lancamento { get; set; }

    // Relacionamento many-to-many com Compositor
    public virtual ICollection<Compositor> Compositores { get; set; } = new List<Compositor>();
}
