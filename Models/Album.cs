namespace ISPMediaAPI.Models;

public class Album
{
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        [StringLength(100)]
        public string Artista { get; set; }

        [StringLength(50)]
        public string Genero { get; set; }

        public DateTime DataLancamento { get; set; }

        public string CaminhoImagemCapa { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string Editora { get; set; }

        [StringLength(100)]
        public string Produtor { get; set; }

        public decimal? Preco { get; set; }

        public bool EstaPublico { get; set; } = true;

        // Relacionamentos
        public int IdUtilizador { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        //public virtual ICollection<FicheiroMultimidia> FicheirosMultimidia { get; set; } = new List<FicheiroMultimidia>();
        //public virtual ICollection<Critica> Criticas { get; set; } = new List<Critica>();
    
}
