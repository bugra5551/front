using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Application.DTOs.CreateUpdate;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Kullanıcı türü CRUD operasyonları için servis arayüzü.
/// </summary>
public interface IUserTypeService : IService<UserTypeCreateUpdateDTO,UserTypeDTO>
{
}
