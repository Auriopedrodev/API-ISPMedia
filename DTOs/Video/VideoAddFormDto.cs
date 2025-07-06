namespace ISPMediaAPI.DTOs.Video;

public class VideoAddFormDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string AutorNome { get; set; } = string.Empty;
    public string ProdutatoraNome { get; set; } = string.Empty;
    public string BandaNome { get; set; } = string.Empty;
    public string? Participacoes { get; set; }
}