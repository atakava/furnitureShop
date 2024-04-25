using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;

namespace FurnitureShop.Service.Interfaces;

public interface IBannerService
{
    Task<IBaseResponse<Banner?>> GetBanner(int id);
    Task<IBaseResponse<IEnumerable<Banner?>>> GetAllBanner();
    Task<IBaseResponse<BannerViewModel>> CreateBanner(BannerViewModel bannerModel);
    Task<IBaseResponse<bool>> DeleteBanner(int id);
}