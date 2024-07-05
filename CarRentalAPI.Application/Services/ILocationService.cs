using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Lokasyon CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ILocationService : IService<LocationCreateUpdateDTO,LocationDTO>  
{
}
