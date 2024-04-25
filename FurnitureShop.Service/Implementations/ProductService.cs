using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Exception = System.Exception;

namespace FurnitureShop.Service.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(IProductRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<IBaseResponse<Product>> GetProduct(int id)
    {
        var baseResponse = new BaseResponse<Product>();

        try
        {
            var product = await _repository.Get(id);

            if (product == null)
            {
                baseResponse.ErrorMesage = "продукт с заданным ID не найден";
                baseResponse.Success = false;

                return baseResponse;
            }

            baseResponse.Success = true;
            baseResponse.Data = product;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<Product>()
            {
                ErrorMesage = $"[Product Get]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<Product>> GetProductByTitle(string title)
    {
        var baseResponse = new BaseResponse<Product>();

        try
        {
            var product = await _repository.GetProductByTitle(title);

            if (product == null)
            {
                baseResponse.ErrorMesage = "продукт с заданным ID не найден";
                baseResponse.Success = false;

                return baseResponse;
            }

            baseResponse.Success = true;
            baseResponse.Data = product;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<Product>()
            {
                ErrorMesage = $"[Product title Get]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<Product>>> GetAllProducts()
    {
        var baseResponse = new BaseResponse<IEnumerable<Product>>();

        try
        {
            var products = await _repository.Select();

            if (!products.Any())
            {
                baseResponse.ErrorMesage = "у вас нет товаров";
                baseResponse.Success = true;

                return baseResponse;
            }

            baseResponse.Data = products;
            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<Product>>()
            {
                ErrorMesage = $"[Product Get List]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<Product>> CreateProduct(ProductViewModel productModel)
    {
        var baseResponse = new BaseResponse<Product>();

        try
        {
            var findProduct = await _repository.GetProductByTitle(productModel.Title);

            if (findProduct != null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Проукт с таким названием уже существует";

                return baseResponse;
            }

            var product = new Product()
            {
                Title = productModel.Title,
                CatalogId = productModel.CatalogId,
                Price = productModel.Price,
                Count = productModel.Count,
                Description = productModel.Description,
                ShortDescription = productModel.ShortDescription,
            };

            if (productModel.Image != null && productModel.Image.Length >= 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productModel.Image.FileName);
                var filePath = Path.Combine("wwwroot/image", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productModel.Image.CopyToAsync(stream);
                }

                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                product.Image = $"{host}/image/{uniqueFileName}";
            }

            await _repository.Create(product);

            baseResponse.Success = true;
            baseResponse.Data = product;

            return baseResponse;
        }

        catch (Exception exception)
        {
            return new BaseResponse<Product>()
            {
                ErrorMesage = $"[Product Create]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<Product>> UpdateProduct(ProductViewModel productViewModel)
    {
        var baseResponse = new BaseResponse<Product>();

        try
        {
            var existingProduct = await _repository.Get(productViewModel.Id);

            if (existingProduct == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Продукт не найден";
                return baseResponse;
            }

            existingProduct.Title = productViewModel.Title;
            existingProduct.CatalogId = productViewModel.CatalogId;
            existingProduct.Price = productViewModel.Price;
            existingProduct.Count = productViewModel.Count;
            existingProduct.Description = productViewModel.Description;
            existingProduct.ShortDescription = productViewModel.ShortDescription;

            if (productViewModel.Image != null && productViewModel.Image.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productViewModel.Image.FileName);
                var filePath = Path.Combine("wwwroot/image", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productViewModel.Image.CopyToAsync(stream);
                }

                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                existingProduct.Image = $"{host}/image/{uniqueFileName}";
            }

            await _repository.UpdateProduct(existingProduct);

            baseResponse.Success = true;
            baseResponse.Data = existingProduct;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<Product>()
            {
                ErrorMesage = $"[Product Update]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteProduct(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            var product = await _repository.Get(id);

            if (product == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Продукта с таким ID не существует";

                return baseResponse;
            }

            await _repository.Delete(product);

            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<bool>()
            {
                ErrorMesage = $"[Product Delete]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<bool>> AddParameterToProduct(int productId, int parameterId)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            await _repository.AddParameterToProduct(productId, parameterId);
            baseResponse.Success = true;
        }
        catch (Exception ex)
        {
            baseResponse.Success = false;
            baseResponse.ErrorMesage = ex.Message;
        }

        return baseResponse;
    }

    public async Task<IBaseResponse<bool>> RemoveParameterFromProduct(int productId, int parameterId)
    {
        var response = new BaseResponse<bool>();

        try
        {
            await _repository.RemoveParameterFromProduct(productId, parameterId);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ErrorMesage = ex.Message;
        }

        return response;
    }
}