namespace ISPMediaAPI.DTOs.Utilizador;

public class UtilizadorAddDTO
{
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public TipoUtilizador Tipo { get; set; }
}