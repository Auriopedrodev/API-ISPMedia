namespace ISPMediaAPI.DTOs.Lancamento;

public class LancamentoAddDTO
{
    public string Titulo { get; set; }

    public string FichaTecnica { get; set; }

    public DateOnly DataLancamento { get; set; }

    public string TipoLancamento { get; set; }
}