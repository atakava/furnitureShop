namespace FurnitureShop.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<T> Create(T entity);
    Task<T?> Get(int id);
    Task<IEnumerable<T?>> Select();
    Task<bool> Delete(T entity);
}