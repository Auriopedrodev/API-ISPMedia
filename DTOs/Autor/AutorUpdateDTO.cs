using ISPMediaAPI.DTOs.ArtistaDTO;
using ISPMediaAPI.DTOs.Banda;
using ISPMediaAPI.DTOs.Produtora;
using ISPMediaAPI.DTOs.ProdutoraDTO;
using ISPMediaAPI.Models;

namespace ISPMediaAPI.DTOs.AutorDTO;

public class AutorUpdateDTO: ArtistaUpdateDTO
{
    public Guid Id { get; set; } 
    public ProdutoraUpdateDTO Produtora { get; set; }
    public ProdutoraUpdateDTO Banda { get; set; }
}
