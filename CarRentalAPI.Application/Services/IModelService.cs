using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Araç modeli CRUD operasyonları için servis arayüzü.
/// </summary>
public interface IModelService : IService<ModelCreateUpdateDTO, ModelDTO> {
    Task<IEnumerable<ModelDTO>> GetModelsByBrandIdAsync(int brandId);
}