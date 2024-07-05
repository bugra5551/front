using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Araç özellikleri CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ICarSpecificationService : IService<CarSpecificationCreateUpdateDTO, CarSpecificationDTO> { }