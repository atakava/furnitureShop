using FurnitureShop.Domain.Entity;

namespace FurnitureShop.Domain.Response;

public interface IBaseResponse<T>
{
        public bool Success { get; set; }
        public string? ErrorMesage { get; set; }
        public T? Data { get; set; }
}