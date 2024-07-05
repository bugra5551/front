using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Araç kiralama koşulları CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ICarRentingConditionService : IService<CarRentingConditionCreateUpdateDTO, CarRentingConditionDTO>
{
    Task<IEnumerable<CarRentingConditionDTO>> GetCarRentingConditionsByCarIdAsync(int carId);
}