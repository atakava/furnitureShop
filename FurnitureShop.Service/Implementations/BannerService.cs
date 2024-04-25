using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FurnitureShop.Service.Implementations;

public class BannerService : IBannerService
{
    private readonly IBannerRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public BannerService(IBannerRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IBaseResponse<Banner?>> GetBanner(int id)
    {
        var baseResponse = new BaseResponse<Banner?>();

        try
        {
            var banner = await _repository.Get(id);

            if (banner == null)
            {
                baseResponse.ErrorMesage = "банера по заданному id не существует";
                baseResponse.Success = false;
            }

            baseResponse.Success = true;
            baseResponse.Data = banner;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<Banner?>()
            {
                ErrorMesage = $"[Banner get] = {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<Banner?>>> GetAllBanner()
    {
        var baseResponse = new BaseResponse<IEnumerable<Banner?>>();

        try
        {
            var banners = await _repository.Select();

            if (!banners.Any())
            {
                baseResponse.ErrorMesage = "у вас нет банеров";
                baseResponse.Success = true;

                return baseResponse;
            }

            baseResponse.Data = banners;
            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<Banner?>>()
            {
                ErrorMesage = $"[Banner all get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<BannerViewModel>> CreateBanner(BannerViewModel bannerModel)
    {
        var baseResponse = new BaseResponse<BannerViewModel>();

        try
        {
            var banner = new Banner()
            {
                Title = bannerModel.Title,
                Text = bannerModel.Text,
                TargetProductPath = bannerModel.TargetProductPath,
            };

            if (bannerModel.Image != null && bannerModel.Image.Length >= 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(bannerModel.Image.FileName);
                var filePath = Path.Combine("wwwroot/image", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bannerModel.Image.CopyToAsync(stream);
                }

                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                banner.ImagePath = $"{host}/image/{uniqueFileName}";
            }
            
            await _repository.Create(banner);

            baseResponse.Success = true;
            baseResponse.Data = bannerModel;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<BannerViewModel>()
            {
                ErrorMesage = $"[Banner Create] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteBanner(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            var user = await _repository.Get(id);

            if (user == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Баннера с таким ID не существует";

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
                ErrorMesage = $"[Banner Delete]: {exception.Message}",
                Success = false
            };
        }
    }
}