using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Araç özellikleri CRUD operasyonları için depo arayüzü.
/// </summary>
public interface ICarSpecificationRepository : IRepository<CarSpecification> { }