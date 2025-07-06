namespace ISPMediaAPI.Models;
using System.ComponentModel.DataAnnotations;

public class Video: Media
{
    [StringLength(100)]
    public string? Formato { get; set; }
    
    public int? Duracao { get; set; } // em segundos
    
    [StringLength(100)]
    public string? Resolucao { get; set; }
}
