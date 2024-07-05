using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Araç sınıfı CRUD operasyonları için depo arayüzü.
/// </summary>
public interface ICarClassRepository : IRepository<CarClass> { }