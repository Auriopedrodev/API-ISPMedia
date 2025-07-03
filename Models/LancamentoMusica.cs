using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;

namespace ISPMediaAPI.Models;

public class LancamentoMusica
{
    private Guid id;

    private string nomeLancamentoMusical;

    private string descricaoLancamentoMusical;

    private Date dataLancamento;

    private string tipoLancamento;

    private string capa;
}
