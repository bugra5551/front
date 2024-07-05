using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Çalışan CRUD operasyonları için depo arayüzü.
/// </summary>
public interface IEmployeeRepository : IRepository<Employee> { }