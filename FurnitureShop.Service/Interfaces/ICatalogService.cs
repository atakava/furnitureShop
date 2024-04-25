using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;

namespace FurnitureShop.Service.Interfaces;

public interface ICatalogService
{
    Task<IBaseResponse<Catalog>> GetCatalog(int id);
    Task<IBaseResponse<Catalog>> GetCatalogByTitle(string title);
    Task<IBaseResponse<IEnumerable<Catalog>>> GetAllCatalogs();
    Task<IBaseResponse<Catalog>> CreateCatalog(CatalogViewModel catalogModel);
    Task<IBaseResponse<bool>> DeleteCatalog(int id);
}