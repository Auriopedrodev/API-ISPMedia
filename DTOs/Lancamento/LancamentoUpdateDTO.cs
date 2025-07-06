using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ISPMediaAPI.DTOs.Lancamento;

public class LancamentoUpdateDTO
{
    public Guid Id { get; set; }

    public string Titulo;

    public string fichaTecnica;

    public DateOnly dataLancamento;

    public string tipoLancamento;

    public string Capa;
}
