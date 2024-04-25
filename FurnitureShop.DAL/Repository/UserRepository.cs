using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDatabaseContext _db;

    public UserRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<User> Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<User?> Get(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<User?>> Select()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<bool> Delete(User entity)
    {
        _db.Users.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<User?> GetUserByPhone(string phone)
    {
        return await _db.Users.FirstOrDefaultAsync(i => i.Phone == phone);
    }
}