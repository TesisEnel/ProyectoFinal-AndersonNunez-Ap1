using ProyectoFinalAp1.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalAp1.Models;

public class CarritoCompra
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(IdUsuario))]
    public string IdUsuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public List<CarritoDetalle> Detalles { get; set; } = new();
    public ApplicationUser Usuario { get; set; }
}
