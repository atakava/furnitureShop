using FurnitureShop.Domain.Request;
using FurnitureShop.Domain.Request.Catalog;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class ParameterController : ControllerBase
{
    private readonly IParameterService _service;

    public ParameterController(IParameterService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] DeleteCatalogRequest request)
    {
        var parameter = await _service.GetParameter(request.Id);

        return Ok(parameter);
    }

    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        var parameters = await _service.GetAllParameters();

        return Ok(parameters);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ParameterViewModel parameterModel)
    {
        var parameter = await _service.CreateParameter(parameterModel);

        return Ok(parameter);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCatalogRequest request)
    {
        await _service.DeleteParameter(request.Id);

        return NoContent();
    }
}