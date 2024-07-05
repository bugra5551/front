using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Araç resimleri CRUD operasyonları için depo arayüzü.
/// </summary>
public interface ICarImageRepository : IRepository<CarImage> 
{
    /// <summary>
    /// Araç ID'sine göre resimleri getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli bir araca ait resimlerin listesi.</returns>
    Task<IEnumerable<CarImage>> GetImagesByCarIdAsync(int carId);

    /// <summary>
    /// Araç ID'sine göre ilk resmi base64 formatında getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Base64 formatında araç resmi.</returns>
    Task<string> GetFirstImageBase64(int carId);
}