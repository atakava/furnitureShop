using FurnitureShop.Domain.Entity;
using FurnitureShop.Domain.Request;
using FurnitureShop.Domain.Response;
using FurnitureShop.Domain.ViewModels;

namespace FurnitureShop.Service.Interfaces;

public interface IParameterService
{
    Task<IBaseResponse<IEnumerable<Parameter>>> GetAllParameters();
    Task<IBaseResponse<Parameter>> GetParameter(int id);
    Task<IBaseResponse<ParameterViewModel>> CreateParameter(ParameterViewModel parameterModel);
    Task<IBaseResponse<bool>> DeleteParameter(int id);
}