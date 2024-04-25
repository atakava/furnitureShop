using Microsoft.AspNetCore.Http;

namespace FurnitureShop.Domain.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IFormFile? Image { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public int CatalogId { get; set; }
}