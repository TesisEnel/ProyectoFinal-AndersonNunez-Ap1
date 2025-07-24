using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalAp1.Models
{
    public class Aviones
    {
        [Key]
        public int IdAvion { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [ForeignKey(nameof(IdCategoria))]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El modelo no puede tener más de 100 caracteres.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El fabricante es obligatorio.")]
        [StringLength(100, ErrorMessage = "El fabricante no puede tener más de 100 caracteres.")]
        public string Fabricante { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La capacidad de pasajeros debe ser al menos 1.")]
        public int CapacidadPasajeros { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "La velocidad máxima debe ser positiva.")]
        public double VelocidadMaxima { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        public string ImagenUrl { set; get; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha de fabricación es obligatoria.")]
        public DateTime FechaFabricacion { get; set; }

        public bool Disponible { get; set; } = true;

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad disponible no puede ser negativa.")]
        public int CantidadDisponible { get; set; }

        public Categoria? Categoria { get; set; }
    }
}
