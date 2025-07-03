using System.ComponentModel.DataAnnotations;

namespace ISPMediaAPI.DTOs.BandaDTO;

public class BandaAddDTO
{
    
        [Required]
        public string Nome { get; set; }
        public string? Biografia { get; set; }
        public DateOnly Inicio { get; set; }
        public DateOnly? Fim { get; set; }
        /*public int? UtilizadorId { get; set; }*/


}
