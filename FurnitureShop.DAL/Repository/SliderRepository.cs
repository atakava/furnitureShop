using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL.Repository;

public class SliderRepository : ISliderRepository
{
    private readonly AppDatabaseContext _db;

    public SliderRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<SliderItem> Create(SliderItem entity)
    {
        await _db.SliderItems.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<SliderItem?> Get(int id)
    {
        return await _db.SliderItems.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<SliderItem?>> Select()
    {
        return await _db.SliderItems.ToListAsync();
    }

    public async Task<bool> Delete(SliderItem entity)
    {
        _db.SliderItems.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}