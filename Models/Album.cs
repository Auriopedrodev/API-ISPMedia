namespace ISPMediaAPI.Models;

public class Album
{
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Classificacao { get; set; }

        public double media = 0;
        public List<Musica> Musicas { get; set; } = new List<Musica>();

}
