using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

namespace ISPMediaAPI.Models;

public class Lancamento
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(255)]
    public string Titulo { get; set; }

    [StringLength(2000)]
    public string FichaTecnica { get; set; }
      
    [Required]
    public DateOnly DataLancamento { get; set; }

    [Required]
    [StringLength(50)]
    public string TipoLancamento { get; set; }

    [StringLength(500)]
    public string Capa { get; set; }
    
    // Relacionamento one-to-many com Musica
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
}
