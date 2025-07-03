using System.ComponentModel.DataAnnotations;

namespace ISPMediaAPI.DTOs.ProdutoraDTO;

public class ProdutoraAddDTO
{

    [Required]
    public string Nome { get; set; }
}
