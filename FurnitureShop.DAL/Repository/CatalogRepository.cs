using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL.Repository;

public class CatalogRepository : ICatalogRepository
{
    private readonly AppDatabaseContext _db;

    public CatalogRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<Catalog> Create(Catalog entity)
    {
        await _db.Catalogs.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Catalog?> Get(int id)
    {
        return await _db.Catalogs
            .Include(p => p.Products)!
            .ThenInclude(pp => pp.ProductParameters)!
            .ThenInclude(p => p.Parameter)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Catalog?>> Select()
    {
        return await _db.Catalogs.Include(p => p.Products)!
            .ThenInclude(pp => pp.ProductParameters)!
            .ThenInclude(p => p.Parameter)
            .ToListAsync();
    }

    public async Task<bool> Delete(Catalog entity)
    {
        _db.Catalogs.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<Catalog?> GetCatalogByTitle(string title)
    {
        return await _db.Catalogs
            .Include(p => p.Products)!
            .ThenInclude(pp => pp.ProductParameters)!
            .ThenInclude(p => p.Parameter).FirstOrDefaultAsync(i => i.Title == title);
    }
}