using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;

namespace ISPMediaAPI.Models;

public class Lancamento
{
    private Guid Id;

    private string Titulo;

    private string fichaTecnica;
      
    private DateOnly dataLancamento;

    private string tipoLancamento;

    private string Capa;
}
