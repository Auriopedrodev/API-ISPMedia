using ISPMediaAPI.DTOs.Lancamento;

namespace ISPMediaAPI.DTOs.Musica;

public class MusicaAddFormDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Letra { get; set; } = string.Empty;
    public string GeneroNome { get; set; } = string.Empty;
    public string AutorNome { get; set; } = string.Empty;
    public string ProdutatoraNome { get; set; } = string.Empty;
    public string BandaNome { get; set; } = string.Empty;
    public string? Compositores { get; set; }
    public string? Participacoes { get; set; }
    public LancamentoAddDTO Lancamento { get; set; }
}