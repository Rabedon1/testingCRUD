using System.ComponentModel.DataAnnotations;
namespace TDDTesting.Models
{ 

    public class Cliente
    {
        [Key]
        public int Codigo { get; set; }
        public required string Cedula { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public bool Estado { get; set; }

    }
}