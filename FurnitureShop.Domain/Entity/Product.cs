using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Domain.Entity;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public int CatalogId { get; set; }
    public virtual Catalog Catalog { get; set; }
    public virtual List<ProductParameter>? ProductParameters { get; set; }
}