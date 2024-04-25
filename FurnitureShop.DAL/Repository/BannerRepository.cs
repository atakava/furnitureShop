using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL.Repository;

public class BannerRepository : IBannerRepository
{
    private readonly AppDatabaseContext _db;

    public BannerRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<Banner> Create(Banner entity)
    {
        await _db.Banners.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Banner?> Get(int id)
    {
        return await _db.Banners.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Banner?>> Select()
    {
        return await _db.Banners.ToListAsync();
    }

    public async Task<bool> Delete(Banner entity)
    {
        _db.Banners.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}