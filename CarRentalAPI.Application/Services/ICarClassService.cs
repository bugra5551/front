using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Araç sınıfı CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ICarClassService : IService<CarClassCreateUpdateDTO, CarClassDTO> { }
