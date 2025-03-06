using System.ComponentModel.DataAnnotations;

namespace TDDTesting.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int ClienteId { get; set; }

        // Relación de muchos a uno con Cliente
        public Cliente Cliente { get; set; }

        // Relación uno a muchos con OpinionesClientes
        public ICollection<OpinionCliente> Opiniones { get; set; }
    }
}
