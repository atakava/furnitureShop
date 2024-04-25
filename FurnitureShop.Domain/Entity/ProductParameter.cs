namespace FurnitureShop.Domain.Entity;

public class ProductParameter
{
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public int ParameterId { get; set; }
    public virtual Parameter Parameter { get; set; }
}