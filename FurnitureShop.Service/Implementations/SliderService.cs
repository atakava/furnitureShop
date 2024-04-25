using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FurnitureShop.Service.Implementations;

public class SliderService : ISliderService
{
    private readonly ISliderRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SliderService(ISliderRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IBaseResponse<IEnumerable<SliderItem>>> GetAllSlides()
    {
        var baseResponse = new BaseResponse<IEnumerable<SliderItem>>();

        try
        {
            var slides = await _repository.Select();

            if (!slides.Any())
            {
                baseResponse.ErrorMesage = "у вас нет слайдов";
                baseResponse.Success = true;

                return baseResponse;
            }

            baseResponse.Data = slides;
            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<SliderItem>>()
            {
                ErrorMesage = $"[Slides all get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<SliderItem?>> GetSlide(int id)
    {
        var baseResponse = new BaseResponse<SliderItem?>();

        try
        {
            var slide = await _repository.Get(id);

            if (slide == null)
            {
                baseResponse.ErrorMesage = "слайда по заданному ID не существует";
                baseResponse.Success = true;

                return baseResponse;
            }

            baseResponse.Data = slide;
            baseResponse.Success = true;

            return baseResponse;
        }

        catch (Exception exception)
        {
            return new BaseResponse<SliderItem?>()
            {
                ErrorMesage = $"[Sliders get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<SliderViewModel>> CreateSlider(SliderViewModel slideModel)
    {
        var baseResponse = new BaseResponse<SliderViewModel>();

        try
        {
            var slide = new SliderItem();

            if (slideModel.Image != null && slideModel.Image.Length >= 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(slideModel.Image.FileName);
                var filePath = Path.Combine("wwwroot/image", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await slideModel.Image.CopyToAsync(stream);
                }

                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                slide.ImagePath = $"{host}/image/{uniqueFileName}";
            }

            await _repository.Create(slide);

            baseResponse.Success = true;
            baseResponse.Data = slideModel;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<SliderViewModel>()
            {
                ErrorMesage = $"[Slider Delete ]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<bool>> RemoveSlide(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            var user = await _repository.Get(id);

            if (user == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Слайда с таким ID не существует";

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
                ErrorMesage = $"[Slider Delete]: {exception.Message}",
                Success = false
            };
        }
    }
}