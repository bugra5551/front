using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Araç resimleri CRUD operasyonları için depo sınıfı.
/// </summary>
public class CarImageRepository : ICarImageRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CarImageRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CarImageRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir araç resmi ekler.
    /// </summary>
    /// <param name="carImage">Eklenecek araç resmi.</param>
    /// <returns>Eklenen araç resmi.</returns>
    public async Task<CarImage> Add(CarImage carImage)
    {
        _context.CarImages.Add(carImage);
        await _context.SaveChangesAsync();
        return carImage;
    }

    /// <summary>
    /// Bir araç resmini siler (işaretler).
    /// </summary>
    /// <param name="carImage">Silinecek araç resmi.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(CarImage carImage)
    {
        var detectedCar = await _context.Cars
            .Where(c => c.CarId == carImage.CarId).FirstOrDefaultAsync();

        if (detectedCar != null) { return false; }

        _context.Entry(carImage).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm araç resimlerini getirir.
    /// </summary>
    /// <returns>Tüm araç resimlerinin listesi.</returns>
    public async Task<IEnumerable<CarImage>> GetAllAsync()
    {
        return await _context.CarImages.ToListAsync();
    }

    /// <summary>
    /// Belirli bir araç resmini ID ile getirir.
    /// </summary>
    /// <param name="carImageId">Getirilecek araç resminin ID'si.</param>
    /// <returns>Belirli araç resminin bilgileri.</returns>
    public async Task<CarImage> GetByIdAsync(int carImageId)
    {
        return await _context.CarImages.Where(b => b.CarImageId == carImageId).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Belirli bir araca ait tüm resimleri getirir.
    /// </summary>
    /// <param name="carId">Aracın ID'si.</param>
    /// <returns>Belirli araca ait tüm resimlerin listesi.</returns>
    public async Task<IEnumerable<CarImage>> GetImagesByCarIdAsync(int carId)
    {
        return await _context.CarImages.Where(b => b.CarId == carId && !b.isDeleted).ToListAsync();
    }

    /// <summary>
    /// Bir araç resmini günceller.
    /// </summary>
    /// <param name="carImage">Güncellenecek araç resmi.</param>
    /// <returns>Güncellenen araç resmi.</returns>
    public async Task<CarImage> Update(CarImage carImage)
    {
        var existingCarImage = await _context.CarImages.FirstOrDefaultAsync(b => b.CarImageId == carImage.CarImageId);
        if (existingCarImage != null)
        {
            _context.Entry(existingCarImage).CurrentValues.SetValues(carImage);
            await _context.SaveChangesAsync();
            return existingCarImage;
        }
        return null;
    }

    /// <summary>
    /// Bir aracın ilk resmini Base64 formatında getirir.
    /// </summary>
    /// <param name="carId">Aracın ID'si.</param>
    /// <returns>Aracın ilk resminin Base64 formatındaki verisi.</returns>
    public async Task<string> GetFirstImageBase64(int carId)
    {
        var image = await _context.CarImages
            .Where(ci => ci.CarId == carId && !ci.isDeleted)
            .FirstOrDefaultAsync();

        return image?.ImageData;
    }
}
