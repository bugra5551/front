using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Kiralama koşulları CRUD operasyonları için depo arayüzü.
/// </summary>
public interface IRentingConditionRepository : IRepository<RentingCondition> { }
