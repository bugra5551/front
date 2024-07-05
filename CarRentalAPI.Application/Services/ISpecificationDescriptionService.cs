using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Özellik açıklaması CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ISpecificationDescriptionService : IService<SpecificationDescriptionCreateUpdateDTO, SpecificationDescriptionDTO> { }