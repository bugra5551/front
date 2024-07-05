using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Kiralama koşulları CRUD operasyonları için servis arayüzü.
/// </summary>
public interface IRentingConditionService : IService<RentingConditionCreateUpdateDTO, RentingConditionDTO> { }
