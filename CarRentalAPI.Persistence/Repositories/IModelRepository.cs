using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Model CRUD operasyonları için depo arayüzü.
/// </summary>
public interface IModelRepository : IRepository<Model> {

    /// <summary>
    /// Marka ID'sine göre modelleri getirir.
    /// </summary>
    /// <param name="brandId">Marka ID'si.</param>
    /// <returns>Belirli bir markaya ait modellerin listesi.</returns>
    Task<IEnumerable<Model>> GetModelsByBrandIdAsync(int brandId);
}
