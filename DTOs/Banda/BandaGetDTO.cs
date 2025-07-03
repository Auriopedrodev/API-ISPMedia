namespace ISPMediaAPI.DTOs.Banda;

public class BandaGetDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } 
    public string? Biografia { get; set; }
    public DateOnly Inicio { get; set; }
    public DateOnly? Fim { get; set; }
}
