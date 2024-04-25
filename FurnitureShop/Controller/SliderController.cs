using FurnitureShop.Domain.Request.Catalog;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class SliderController : ControllerBase
{
    private readonly ISliderService _service;

    public SliderController(ISliderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        var slides = await _service.GetAllSlides();

        return Ok(slides);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] DeleteCatalogRequest request)
    {
        var slide = await _service.GetSlide(request.Id);

        return Ok(slide);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] SliderViewModel sliderModel)
    {
        var slide = await _service.CreateSlider(sliderModel);

        return Ok(slide);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCatalogRequest request)
    {
        await _service.RemoveSlide(request.Id);

        return NoContent();
    }
}