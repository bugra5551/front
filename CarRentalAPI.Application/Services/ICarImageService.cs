using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Araç resimleri CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ICarImageService :IService<CarImageCreateUpdateDTO, CarImageDTO>
{
    Task<IEnumerable<CarImageDTO>> GetImagesByCarIdAsync(int carId);
    Task<string> GetFirstImageBase64(int carId);

}