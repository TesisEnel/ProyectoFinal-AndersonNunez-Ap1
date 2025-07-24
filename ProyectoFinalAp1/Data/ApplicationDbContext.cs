using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalAp1.Models;

namespace ProyectoFinalAp1.Data
{
  

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aviones> Avions { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarritoCompra> CarritoCompras { get; set; }
        public DbSet<CarritoDetalle> CarritoDetalles { get; set; }



    }
}
