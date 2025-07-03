namespace ISPMediaAPI.Models;

public class Autor: Artista
{
    public Guid ProdutoraId { get; set; }
    [ForeignKey(nameof(ProdutoraId))]
    public Produtora Produtora { get; set; }

    public Guid BandaId { get; set; }
    [ForeignKey(nameof(BandaId))]
    public Banda Banda { get; set; }
    


    /*
      public int CategoriaId { get; set; }
        [ForeignKey(nameof(CategoriaId))]
        public Categoria Categoria { get; set; }
     
     */
}
