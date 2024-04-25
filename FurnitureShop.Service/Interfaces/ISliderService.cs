using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace FurnitureShop.Service.Interfaces;

public interface ISliderService
{
    Task<IBaseResponse<IEnumerable<SliderItem>>> GetAllSlides();
    Task<IBaseResponse<SliderItem?>> GetSlide(int id);
    Task<IBaseResponse<SliderViewModel>> CreateSlider(SliderViewModel slideModel);
    Task<IBaseResponse<bool>> RemoveSlide(int id);
}