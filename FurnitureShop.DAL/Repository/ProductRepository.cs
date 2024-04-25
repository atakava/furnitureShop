using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDatabaseContext _db;

    public ProductRepository(AppDatabaseContext db)
    {
        _db = db;
    }

    public async Task<Product> Create(Product entity)
    {
        await _db.Products.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Product?> Get(int id)
    {
        return await _db.Products
            .Include(p => p.ProductParameters)!
            .ThenInclude(p => p.Parameter)
            .Include(c => c.Catalog).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Product?>> Select()
    {
        return await _db.Products
            .Include(p => p.ProductParameters)!
            .ThenInclude(p => p.Parameter)
            .Include(c => c.Catalog).ToListAsync();
    }

    public async Task<bool> Delete(Product entity)
    {
        _db.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<Product?> GetProductByTitle(string title)
    {
        return await _db.Products.FirstOrDefaultAsync(i => i.Title == title);
    }

    public async Task AddParameterToProduct(int productId, int parameterId)
    {
        var existingRelation = await _db.ProductParameters
            .FirstOrDefaultAsync(pp => pp.ProductId == productId && pp.ParameterId == parameterId);

        if (existingRelation != null)
            return;

        var productParameter = new ProductParameter { ProductId = productId, ParameterId = parameterId };
        _db.ProductParameters.Add(productParameter);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveParameterFromProduct(int productId, int parameterId)
    {
        var productParameter = await _db.ProductParameters
            .FirstOrDefaultAsync(pp => pp.ProductId == productId && pp.ParameterId == parameterId);

        if (productParameter != null)
        {
            _db.ProductParameters.Remove(productParameter);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> UpdateProduct(Product entity)
    {
        var entry = _db.Attach(entity);
        entry.State = EntityState.Modified;
        
        await _db.SaveChangesAsync();

        return true;
    }
}