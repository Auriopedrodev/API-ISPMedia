namespace ISPMediaAPI.DTOs.Autor;

public class AutorGetDTO : ArtistaGetDTO
{
    public Guid Id { get; set; }
    public ProdutoraGetDTO Produtora { get; set; }
    public BandaGetDTO Banda { get; set; }
}