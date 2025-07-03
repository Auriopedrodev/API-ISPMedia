namespace ISPMediaAPI.DTOs.Utilizador;

public class UtilizadorUpdateDTO
{
    public Guid Id { get; set; }
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}
