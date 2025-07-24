using Microsoft.EntityFrameworkCore;
using ProyectoFinalAp1.Data;
using ProyectoFinalAp1.Models;
using System.Linq.Expressions;

namespace ProyectoFinalAp1.Services;

public class CategoriaService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public CategoriaService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Categoria categoria)
    {
        if (!await Existe(categoria.IdCategoria))
            return await Insertar(categoria);
        else
            return await Modificar(categoria);
    }

    public async Task<bool> Existe(int categoriaId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Categorias.AnyAsync(c => c.IdCategoria == categoriaId);
    }

    private async Task<bool> Insertar(Categoria categoria)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        contexto.Categorias.Add(categoria);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Categoria categoria)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        contexto.Categorias.Update(categoria);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Categoria?> Buscar(int categoriaId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == categoriaId);
    }

    public async Task<bool> Eliminar(int categoriaId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        var categoria = await contexto.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == categoriaId);
        if (categoria != null)
        {
            contexto.Categorias.Remove(categoria);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }
    public async Task<List<Categoria>> Listar()
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Categorias.ToListAsync();
    }
    public async Task<List<Categoria>> Listar(Expression<Func<Categoria, bool>> criterio)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Categorias.Where(criterio).ToListAsync();
    }

    public async Task<bool> ExisteCategoria(int categoriaId, string nombre)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Categorias
            .AnyAsync(c => c.IdCategoria != categoriaId && c.Nombre.ToLower() == nombre.ToLower());
    }
}
