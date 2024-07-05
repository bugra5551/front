using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Müşteri CRUD operasyonları için depo arayüzü.
/// </summary>
public interface ICustomerRepository : IRepository<Customer> { }
