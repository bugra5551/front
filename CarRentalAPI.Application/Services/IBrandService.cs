using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Marka CRUD operasyonları için servis arayüzü.
/// </summary>
public interface IBrandService : IService<BrandCreateUpdateDTO , BrandDTO> { }