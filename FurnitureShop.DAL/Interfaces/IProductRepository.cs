using FurnitureShop.Domain.Entity;

namespace FurnitureShop.DAL.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetProductByTitle(string title);
    Task AddParameterToProduct(int productId, int parameterId);
    Task RemoveParameterFromProduct(int productId, int parameterId);
    Task<bool> UpdateProduct(Product entity);
}