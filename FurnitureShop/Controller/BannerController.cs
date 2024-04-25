using FurnitureShop.Domain.Request.Catalog;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class BannerController : ControllerBase
{
    private readonly IBannerService _service;

    public BannerController(IBannerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] DeleteCatalogRequest request)
    {
        var banner = await _service.GetBanner(request.Id);

        return Ok(banner);
    }

    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        var banners = await _service.GetAllBanner();

        return Ok(banners);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] BannerViewModel bannerModel)
    {
        var banner = await _service.CreateBanner(bannerModel);

        return Ok(banner);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCatalogRequest request)
    {
        await _service.DeleteBanner(request.Id);

        return NoContent();
    }
}