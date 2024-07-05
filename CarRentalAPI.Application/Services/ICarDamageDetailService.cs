using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Araç hasar detayı CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ICarDamageDetailService : IService<CarDamageDetailCreateUpdateDTO, CarDamageDetailDTO>
{
    Task<IEnumerable<CarDamageDetailDTO>> GetDetailsByCarIdAsync(int carId);
}