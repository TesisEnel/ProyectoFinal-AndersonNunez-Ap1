using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalAp1.Models;

public class CarritoDetalle
{
    public int Id { get; set; }
    public int IdAvion { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Total => Cantidad * Precio;

    [ForeignKey(nameof(IdAvion))]
    public Aviones? Avion { get; set; }
}
