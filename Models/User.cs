using System.ComponentModel.DataAnnotations;

namespace UserMgmntAPI.Models
{
    public class User
    {
        public int Id { get; set; }                 // Identificador único
        
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }   // Nombre del usuario
        
        [EmailAddress]
        public string? Email { get; set; }          // Correo electrónico
        
        [Range(0, 120)]
        public int Age { get; set; }                // Edad
    }
}
