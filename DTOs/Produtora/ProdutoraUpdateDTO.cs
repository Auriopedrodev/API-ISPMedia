using System.ComponentModel.DataAnnotations;

namespace ISPMediaAPI.DTOs.ProdutoraDTO;

public class ProdutoraUpdateDTO
{

    [Key]
    public long Id { get; set; }

    [Required]
    public string Nome { get; set; }

}
