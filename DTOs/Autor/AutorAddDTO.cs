using ISPMediaAPI.DTOs.ArtistaDTO;
using ISPMediaAPI.DTOs.ProdutoraDTO;
using ISPMediaAPI.Models;

namespace ISPMediaAPI.DTOs.AutorDTO;

public class AutorAddDTO: ArtistaAddDTO
{

    public ProdutoraAddDTO Produtora { get; set; }
    public ProdutoraAddDTO Banda { get; set; }

}
