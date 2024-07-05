using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Araç CRUD operasyonları için depo arayüzü.
/// </summary>
public interface ICarRepository : IRepository<Car> { }
