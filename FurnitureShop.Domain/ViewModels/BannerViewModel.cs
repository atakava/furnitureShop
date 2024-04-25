using Microsoft.AspNetCore.Http;

namespace FurnitureShop.Domain.ViewModels;

public class BannerViewModel
{
    public string Title { get; set; }
    public string Text { get; set; }
    public IFormFile Image { get; set; }
    public string TargetProductPath { get; set; }
}