using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Araç kiralama koşulları CRUD operasyonları için depo arayüzü.
/// </summary>
public interface ICarRentingConditionRepository : IRepository<CarRentingCondition>
{
    /// <summary>
    /// Araç ID'sine göre kiralama koşullarını getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli bir araca ait kiralama koşullarının listesi.</returns>
    Task<IEnumerable<CarRentingCondition>> GetCarRentingConditionsByCarId(int carId);
}
