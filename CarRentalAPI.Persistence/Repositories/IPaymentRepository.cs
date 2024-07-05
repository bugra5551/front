using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Ödeme CRUD operasyonları için depo arayüzü.
/// </summary>
public interface IPaymentRepository : IRepository<Payment> { }
