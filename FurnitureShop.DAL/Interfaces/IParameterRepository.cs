using FurnitureShop.Domain.Entity;

namespace FurnitureShop.DAL.Interfaces;

public interface IParameterRepository : IBaseRepository<Parameter>
{
    Task<Parameter?> GetParameterByName(string name);
}