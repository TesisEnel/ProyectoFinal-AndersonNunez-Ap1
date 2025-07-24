using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalAp1.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }
    }
}
