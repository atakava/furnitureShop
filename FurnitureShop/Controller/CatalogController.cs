using FurnitureShop.Domain.Request.Catalog;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _service;

    public CatalogController(ICatalogService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        var catalogs = await _service.GetAllCatalogs();

        return Ok(catalogs.Data!.Select(c => new
        {
            c.Id,
            c.Title,
            Product = c.Products!.Select(p => new
            {
                p.Id,
                p.Title,
                p.CatalogId,
                p.Description,
                p.ShortDescription,
                p.Price,
                p.Count,
                p.Image,
                ProductParameters = p.ProductParameters!.Select(i => new
                {
                    i.Parameter.Id,
                    i.Parameter.Name,
                    i.Parameter.Value,
                })
            })
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Get(int id)
    {
        var catalog = await _service.GetCatalog(id);

        return Ok(new
        {
            catalog.Data!.Id,
            catalog.Data!.Title,
            Product = catalog.Data.Products!.Select(p => new
            {
                p.Id,
                p.Title,
                p.CatalogId,
                p.Description,
                p.ShortDescription,
                p.Price,
                p.Count,
                p.Image,
                ProductParameters = p.ProductParameters!.Select(i => new
                {
                    i.Parameter.Id,
                    i.Parameter.Name,
                    i.Parameter.Value,
                })
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetByTitle([FromBody] GetTitleRequest request)
    {
        var catalog = await _service.GetCatalogByTitle(request.Title);

        return Ok(new
        {
            catalog.Data!.Id,
            catalog.Data!.Title,
            Product = catalog.Data.Products!.Select(p => new
            {
                p.Id,
                p.Title,
                p.CatalogId,
                p.Description,
                p.ShortDescription,
                p.Price,
                p.Count,
                p.Image,
                ProductParameters = p.ProductParameters!.Select(i => new
                {
                    i.Parameter.Id,
                    i.Parameter.Name,
                    i.Parameter.Value,
                })
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CatalogViewModel request)
    {
        var catalog = await _service.CreateCatalog(request);

        return Ok(catalog);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCatalogRequest request)
    {
        await _service.DeleteCatalog(request.Id);

        var catalogs = await GetAll();

        return Ok(catalogs);
    }
}