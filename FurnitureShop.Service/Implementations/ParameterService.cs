using FurnitureShop.DAL.Interfaces;
using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Request;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;
using FurnitureShop.Service.Interfaces;

namespace FurnitureShop.Service.Implementations;

public class ParameterService : IParameterService
{
    private readonly IParameterRepository _repository;

    public ParameterService(IParameterRepository repository)
    {
        _repository = repository;
    }

    public async Task<IBaseResponse<IEnumerable<Parameter>>> GetAllParameters()
    {
        var baseResponse = new BaseResponse<IEnumerable<Parameter>>();

        try
        {
            var parameters = await _repository.Select();

            if (!parameters.Any())
            {
                baseResponse.ErrorMesage = "у вас нет параметров";
                baseResponse.Success = true;

                return baseResponse;
            }

            baseResponse.Data = parameters;
            baseResponse.Success = true;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<Parameter>>()
            {
                ErrorMesage = $"[Parameters all get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Parameter>> GetParameter(int id)
    {
        var baseResponse = new BaseResponse<Parameter>();

        try
        {
            var parameter = await _repository.Get(id);

            if (parameter == null)
            {
                baseResponse.ErrorMesage = "параметра с таким ID не найдено";
                baseResponse.Success = false;

                return baseResponse;
            }

            baseResponse.Data = parameter;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<Parameter>()
            {
                ErrorMesage = $"[Parameter get] = {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<ParameterViewModel>> CreateParameter(ParameterViewModel parameterModel)
    {
        var baseResponse = new BaseResponse<ParameterViewModel>();

        try
        {

            var parameter = new Parameter()
            {
                Name = parameterModel.Name,
                Value = parameterModel.Value
            };

            await _repository.Create(parameter);

            baseResponse.Success = true;
            baseResponse.Data = parameterModel;

            return baseResponse;
        }

        catch (Exception exception)
        {
            return new BaseResponse<ParameterViewModel>()
            {
                ErrorMesage = $"[Parameter Create]: {exception.Message}",
                Success = false
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteParameter(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            var user = await _repository.Get(id);

            if (user == null)
            {
                baseResponse.Success = false;
                baseResponse.ErrorMesage = "Параметр с таким ID не существует";

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
                ErrorMesage = $"[Parameter Delete]: {exception.Message}",
                Success = false
            };
        }
    }
}