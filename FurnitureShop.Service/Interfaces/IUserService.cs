using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;

namespace FurnitureShop.Service.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<User>> GetUser(int id);
    Task<IBaseResponse<IEnumerable<User>>> GetAllUser();
    Task<IBaseResponse<UserViewModel>> CreateUser(UserViewModel userModel);
    Task<IBaseResponse<bool>> DeleteUser(int id);

}