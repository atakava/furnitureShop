using FurnitureShop.Domain.Entity;

namespace FurnitureShop.DAL.Interfaces;

public interface ICatalogRepository : IBaseRepository<Catalog>
{
    Task<Catalog?> GetCatalogByTitle(string title);
}