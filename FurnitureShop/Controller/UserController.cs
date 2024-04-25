using System.Security.Cryptography;
using System.Text;
using FurnitureShop.DAL;
using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Request.Catalog;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly AppDatabaseContext _db;

    public UserController(IUserService service, AppDatabaseContext db)
    {
        _service = service;
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllUser();

        return Ok(users.Data!.Select(i => new
        {
            i.Id,
            i.Name,
            i.Phone
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _service.GetUser(id);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserViewModel userModer)
    {
        var user = await _service.CreateUser(userModer);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCatalogRequest request)
    {
        await _service.DeleteUser(request.Id);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> SingIn([FromBody] Admin admin)
    {
        var id = 1;

        var adminData = await _db.Admins.FirstOrDefaultAsync(i => i.Id == id);

        if (admin.Name != adminData.Name || HashPassword(admin.Password) != adminData.Password)
        {
            return Ok(new
            {
                Success = false
            });
        }

        return Ok(new
        {
            Success = true
        });
    }

    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}