using FurnitureShop.Domain.Entity;

namespace FurnitureShop.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByPhone(string name);
}