using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Rezervasyon CRUD operasyonları için depo arayüzü.
/// </summary>
public interface IReservationRepository : IRepository<Reservation>
{
    /// <summary>
    /// Kullanıcı ID'sine göre rezervasyonları getirir.
    /// </summary>
    /// <param name="userId">Kullanıcı ID'si.</param>
    /// <returns>Kullanıcıya ait rezervasyonların listesi.</returns>
    Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId);
}
