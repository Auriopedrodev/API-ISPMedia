namespace ISPMediaAPI.Models;

public class Utilizador
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string PrimeiroNome { get; set; }

    [Required]
    [StringLength(100)]
    public string UltimoNome { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [StringLength(255)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    public TipoUtilizador Tipo { get; set; }

    [Required]
    public bool Estado { get; set; }

}
