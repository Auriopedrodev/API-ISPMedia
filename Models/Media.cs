namespace ISPMediaAPI.Models;

public class Media
{
    public Guid Id { get; set; }

    [Required]
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    [Required]
    public string CaminhoMedia { get; set; }

    public long Tamanho { get; set; }

    public string TipoMedia { get; set; }
    public Autor Autor { get; set; }

    public List<Participacao> Participacoes { get; set; } = new List<Participacao>();

}