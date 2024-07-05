using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Araç hasar detayları CRUD operasyonları için depo arayüzü.
/// </summary>
public interface ICarDamageDetailRepository : IRepository<CarDamageDetail>
{
    /// <summary>
    /// Araç ID'sine göre hasar detaylarını getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli bir araca ait hasar detaylarının listesi.</returns>
    Task<IEnumerable<CarDamageDetail>> GetDetailsByCarIdAsync(int carId);
}