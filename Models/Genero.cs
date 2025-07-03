namespace ISPMediaAPI.Models;

public class Genero
{
    public Guid Id { get; set; }  = Guid.NewGuid();
    public string Nome { get; set; }
}
