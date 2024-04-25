using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Request;
using FurnitureShop.Domain.Request.Catalog;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllProducts();

        return Ok(new
        {
            Products = products.Data!.Select(p => new
            {
                p.Id,
                p.Title,
                p.CatalogId,
                Catalog = new
                {
                    p.Catalog.Title
                },
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
    public async Task<IActionResult> Get([FromBody] DeleteCatalogRequest request)
    {
        var product = await _service.GetProduct(request.Id);

        return Ok(new
        {
            product.Data!.Id,
            product.Data.Title,
            product.Data.CatalogId,
            Catalog = new
            {
                product.Data.Catalog.Title
            },
            product.Data.Description,
            product.Data.ShortDescription,
            product.Data.Price,
            product.Data.Count,
            product.Data.Image,
            ProductParameters = product.Data.ProductParameters!.Select(i => new
            {
                i.Parameter.Id,
                i.Parameter.Name,
                i.Parameter.Value,
            })
        });
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ProductViewModel productModel)
    {
        var product = await _service.CreateProduct(productModel);

        return Ok(new
        {
            product.Data!.Id,
            product.Data.Title,
            product.Data.CatalogId,
            product.Data.Description,
            product.Data.ShortDescription,
            product.Data.Price,
            product.Data.Count,
            product.Data.Image
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] ProductViewModel productModel)
    {
        var product = await _service.UpdateProduct(productModel);

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCatalogRequest request)
    {
        await _service.DeleteProduct(request.Id);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddParameterToProduct([FromBody] AddParameterRequest request)
    {
        var result = await _service.AddParameterToProduct(request.ProductId, request.ParameterId);

        if (result.Success)
            return Ok(result);

        return BadRequest(result.ErrorMesage);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveParameterFromProduct([FromBody] AddParameterRequest request)
    {
        var result = await _service.RemoveParameterFromProduct(request.ProductId, request.ParameterId);

        if (result.Success)
            return Ok(result);

        return BadRequest(result.ErrorMesage);
    }
}