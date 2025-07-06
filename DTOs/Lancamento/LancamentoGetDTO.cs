using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ISPMediaAPI.DTOs.Lancamento;

public class LancamentoGetDTO
{
    public Guid Id;

    public string Titulo;

    public string fichaTecnica;

    public DateOnly dataLancamento;

    public string tipoLancamento;

    public string Capa;
}
