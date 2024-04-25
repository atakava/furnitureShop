using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;

namespace FurnitureShop.Service.Interfaces;

public interface IProductService
{
    Task<IBaseResponse<Product>> GetProduct(int id);
    Task<IBaseResponse<Product>> GetProductByTitle(string title);
    Task<IBaseResponse<IEnumerable<Product>>> GetAllProducts();
    Task<IBaseResponse<Product>> CreateProduct(ProductViewModel productModel);
    Task<IBaseResponse<Product>> UpdateProduct(ProductViewModel productViewModel);
    Task<IBaseResponse<bool>> DeleteProduct(int id);
    Task<IBaseResponse<bool>> AddParameterToProduct(int productId, int parameterId);
    Task<IBaseResponse<bool>> RemoveParameterFromProduct(int productId, int parameterId);
}