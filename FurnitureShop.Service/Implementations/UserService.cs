using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;

namespace FurnitureShop.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IBaseResponse<User>> GetUser(int id)
    {
        var baseResponse = new BaseResponse<User>();

        try
        {
            var user = await _repository.Get(id);

            if (user == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "пользователь с заданным ID не найден";
                return baseResponse;
            }

            baseResponse.Success = true;
            baseResponse.Data = user;
            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<User>()
            {
                ErrorMesage = $"[User get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<User>>> GetAllUser()
    {
        var baseResponse = new BaseResponse<IEnumerable<User>>();

        try
        {
            var userList = await _repository.Select();

            if (!userList.Any())
            {
                baseResponse.ErrorMesage = "у вас нет товаров";
                baseResponse.Success = true;

                return baseResponse;
            }

            baseResponse.Data = userList;
            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<User>>()
            {
                ErrorMesage = $"[User Get List]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteUser(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            var user = await _repository.Get(id);

            if (user == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "пользователь с таким ID не существует";

                return baseResponse;
            }
            
            await _repository.Delete(user);

            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<bool>()
            {
                ErrorMesage = $"[User Delete]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<UserViewModel>> CreateUser(UserViewModel userModel)
    {
        var baseResponse = new BaseResponse<UserViewModel>();

        try
        {
            var findUser = await _repository.GetUserByPhone(userModel.Phone);

            if (findUser != null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Пользователь с таким телефоном уже зарегестрирован";

                return baseResponse;
            }

            var user = new User()
            {
                Phone = userModel.Phone,
                Name = userModel.Name
            };

            await _repository.Create(user);
            
            baseResponse.Success = true;
            baseResponse.Data = userModel;

            return baseResponse;
        }

        catch (Exception exception)
        {
            return new BaseResponse<UserViewModel>()
            {
                ErrorMesage = $"[User Create]: {exception.Message}",
                Success = false
            };
        }
    }
}