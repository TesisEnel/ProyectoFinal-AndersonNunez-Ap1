using Microsoft.EntityFrameworkCore;
using ProyectoFinalAp1.Data;
using ProyectoFinalAp1.Models;
using System.Linq.Expressions;

namespace ProyectoFinalAp1.Services;

public class CarritoCompraService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public CarritoCompraService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Guardar(CarritoCompra carrito)
    {
        if (!await Existe(carrito.Id))
            return await Insertar(carrito);
        else
            return await Modificar(carrito);
    }

    private async Task<bool> Existe(int carritoId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.CarritoCompras.AnyAsync(c => c.Id == carritoId);
    }

    private async Task<bool> Insertar(CarritoCompra carrito)
    {
        await using var contexto = _dbFactory.CreateDbContext();

        contexto.CarritoCompras.Add(carrito);

        foreach (var detalle in carrito.Detalles)
        {
            var avion = await contexto.Avions.FindAsync(detalle.IdAvion);
            if (avion == null)
            {

                throw new Exception($"Avión con Id {detalle.IdAvion} no encontrado.");
            }

            if (avion.CantidadDisponible < detalle.Cantidad)
            {

                throw new Exception($"Inventario insuficiente para el avión {avion.Modelo}. Disponible: {avion.CantidadDisponible}, solicitado: {detalle.Cantidad}");
            }

            avion.CantidadDisponible -= detalle.Cantidad;
            contexto.Avions.Update(avion);
        }


        var resultado = await contexto.SaveChangesAsync();

        return resultado > 0;
    }


    private async Task<bool> Modificar(CarritoCompra carrito)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        contexto.CarritoCompras.Update(carrito);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<CarritoCompra?> Buscar(int carritoId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.CarritoCompras
            .Include(c => c.Detalles)
            .ThenInclude(d => d.IdAvion)
            .FirstOrDefaultAsync(c => c.Id == carritoId);
    }

    public async Task<bool> Eliminar(int carritoId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        var carrito = await contexto.CarritoCompras.FirstOrDefaultAsync(c => c.Id == carritoId);
        if (carrito != null)
        {
            contexto.CarritoCompras.Remove(carrito);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<CarritoCompra>> Listar(Expression<Func<CarritoCompra, bool>> criterio)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.CarritoCompras
            .Include(c => c.Detalles)
            .ThenInclude(d => d.IdAvion)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<CarritoCompra>> ListarConDetalle()
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.CarritoCompras
            .Include(c => c.Detalles)
            .ThenInclude(d => d.Avion).ThenInclude(c => c.Categoria)
           .Include(c => c.Usuario)
            .ToListAsync();
    }

    public async Task<List<CarritoCompra>> ListarConDetalle(string userId)
    {
        await using var contexto = _dbFactory.CreateDbContext();

        return await contexto.CarritoCompras
            .Include(c => c.Detalles)
            .ThenInclude(d => d.Avion)
            .Where(c => c.IdUsuario == userId)
            .ToListAsync();
    }


}
