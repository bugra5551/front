using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Araç CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ICarService : IService<CarCreateUpdateDTO, CarDTO> { }
