using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL.Repository;

public class ParameterRepository : IParameterRepository
{
    private readonly AppDatabaseContext _db;

    public ParameterRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<Parameter> Create(Parameter entity)
    {
        await _db.Parameters.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Parameter?> Get(int id)
    {
        return await _db.Parameters.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Parameter?>> Select()
    {
        return await _db.Parameters.ToListAsync();
    }

    public async Task<bool> Delete(Parameter entity)
    {
        _db.Parameters.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<Parameter?> GetParameterByName(string name)
    {
        return await _db.Parameters.FirstOrDefaultAsync(i => i.Name == name);
    }
}