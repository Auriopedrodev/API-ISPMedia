using ISPMediaAPI.DTOs.Banda;
using ISPMediaAPI.DTOs.Produtora;
using ISPMediaAPI.DTOs.ProdutoraDTO;

namespace ISPMediaAPI.DTOs.Autor
{
    public class AutorGetDTO:ArtistaGetDTO
    {
        public Guid Id { get; set; } 
        public ProdutoraGetDTO Produtora { get; set; }
        public ProdutoraGetDTO Banda { get; set; }
    }
}
