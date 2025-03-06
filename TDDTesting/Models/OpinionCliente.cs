using System.ComponentModel.DataAnnotations;

namespace TDDTesting.Models
{
    public class OpinionCliente
    {
        [Key]
        public int OpinionID { get; set; }

        [Required]
        public int ClienteID { get; set; }

        public int? ProductoID { get; set; }

        [Required]
        [Range(1, 5)]
        public int Calificacion { get; set; }

        public string Comentario { get; set; }

        public DateTime Fecha { get; set; }

        // Relación de muchos a uno con Cliente
        public Cliente Cliente { get; set; }

        // Relación de muchos a uno con Producto
        public Producto Producto { get; set; }
    }
}
