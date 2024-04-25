using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;

namespace FurnitureShop.Service.Implementations;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _repository;

    public CatalogService(ICatalogRepository repository)
    {
        _repository = repository;
    }

    public async Task<IBaseResponse<Catalog>> GetCatalog(int id)
    {
        var baseResponse = new BaseResponse<Catalog>();

        try
        {
            var catalog = await _repository.Get(id);

            if (catalog == null)
            {
                baseResponse.ErrorMesage = "раздела каталога с таким ID не найдено";
                baseResponse.Success = false;

                return baseResponse;
            }

            baseResponse.Data = catalog;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<Catalog>()
            {
                ErrorMesage = $"[Catalog get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Catalog>> GetCatalogByTitle(string title)
    {
        var baseResponse = new BaseResponse<Catalog>();

        try
        {
            var catalog = await _repository.GetCatalogByTitle(title);

            if (catalog == null)
            {
                baseResponse.ErrorMesage = "раздела каталога с таким title не найдено";
                baseResponse.Success = false;

                return baseResponse;
            }

            baseResponse.Data = catalog;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<Catalog>()
            {
                ErrorMesage = $"[Catalog by title get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<Catalog>>> GetAllCatalogs()
    {
        var baseResponse = new BaseResponse<IEnumerable<Catalog>>();

        try
        {
            var userList = await _repository.Select();

            if (!userList.Any())
            {
                baseResponse.ErrorMesage = "у вас нет товаров";
                baseResponse.Success = true;

                return baseResponse;
            }

            baseResponse.Data = userList;
            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<Catalog>>()
            {
                ErrorMesage = $"[Catalog all get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Catalog>> CreateCatalog(CatalogViewModel catalogModel)
    {
        var baseResponse = new BaseResponse<Catalog>();

        try
        {
            var findUser = await _repository.GetCatalogByTitle(catalogModel.Title);

            if (findUser != null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Каталог с таким названием уже существует";

                return baseResponse;
            }

            var catalog = new Catalog()
            {
                Title = catalogModel.Title
            };

            await _repository.Create(catalog);

            baseResponse.Success = true;
            baseResponse.Data = new Catalog
            {
                Id = catalog.Id,
                Title = catalog.Title,
                Products = catalog.Products
            };

            return baseResponse;
        }

        catch (Exception exception)
        {
            return new BaseResponse<Catalog>()
            {
                ErrorMesage = $"[Catalog Create]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteCatalog(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            var user = await _repository.Get(id);

            if (user == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Каталога с таким ID не существует";

                return baseResponse;
            }

            await _repository.Delete(user);

            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<bool>()
            {
                ErrorMesage = $"[Catalog Delete]: {exception.Message}",
                Success = false
            };
        }
    }
}