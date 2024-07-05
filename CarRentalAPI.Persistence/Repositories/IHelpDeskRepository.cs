using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Yardım masası mesajları CRUD operasyonları için depo arayüzü.
/// </summary>
public interface IHelpDeskRepository
{
    /// <summary>
    /// Araç ID'si ve kullanıcı ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <param name="userId">Kullanıcı ID'si.</param>
    /// <returns>Belirli bir araç ve kullanıcıya ait mesajların listesi.</returns>
    Task<IEnumerable<HelpDesk>> GetMessagesByCarIdAndUserId(int carId, int userId);

    /// <summary>
    /// Araç ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli bir araca ait mesajların listesi.</returns>
    Task<IEnumerable<HelpDesk>> GetMessagesByCarId(int carId);

    /// <summary>
    /// Yeni bir mesaj ekler.
    /// </summary>
    /// <param name="message">Eklenecek mesaj.</param>
    /// <returns>Eklenen mesaj.</returns>
    Task<HelpDesk> AddMessage(HelpDesk message);
}
