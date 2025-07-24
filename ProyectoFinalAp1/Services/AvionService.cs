using Microsoft.EntityFrameworkCore;
using ProyectoFinalAp1.Data;
using ProyectoFinalAp1.Models;
using System.Linq.Expressions;

namespace ProyectoFinalAp1.Services;

public class AvionService
{
    
private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public AvionService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Aviones avion)
    {
        if (!await Existe(avion.IdAvion))
            return await Insertar(avion);
        else
            return await Modificar(avion);
    }

    private async Task<bool> Existe(int avionId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Avions.AnyAsync(a => a.IdAvion == avionId);
    }

    private async Task<bool> Insertar(Aviones avion)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        contexto.Avions.Add(avion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Aviones avion)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        contexto.Avions.Update(avion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Aviones?> Buscar(int avionId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Avions.FirstOrDefaultAsync(a => a.IdAvion == avionId);
    }

    public async Task<bool> Eliminar(int avionId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        var avion = await contexto.Avions.FirstOrDefaultAsync(a => a.IdAvion == avionId);
        if (avion != null)
        {
            contexto.Avions.Remove(avion);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Aviones>> Listar(Expression<Func<Aviones, bool>> criterio)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Avions.Where(criterio).Include(a => a.Categoria).ToListAsync();
    }

    public async Task<bool> ExisteAvion(int avionId, string nombre)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Avions.Include(a => a.Categoria)
            .AnyAsync(a => a.IdAvion != avionId && a.Nombre.ToLower() == nombre.ToLower());
    }
}
