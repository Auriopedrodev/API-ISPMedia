namespace ISPMediaAPI.Models;

public class Utilizador
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string PrimeiroNome { get; set; }

    [Required]
    public string UltimoNome { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public TipoUtilizador Tipo { get; set; }

    [Required]
    public bool Estado { get; set; }

}
