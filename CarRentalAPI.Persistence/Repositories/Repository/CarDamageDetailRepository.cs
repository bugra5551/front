using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Araç hasar detaylarının CRUD operasyonları için depo sınıfı.
/// </summary>
public class CarDamageDetailRepository : ICarDamageDetailRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CarDamageDetailRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CarDamageDetailRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir araç hasar detayı ekler.
    /// </summary>
    /// <param name="carDamageDetail">Eklenecek araç hasar detayı.</param>
    /// <returns>Eklenen araç hasar detayı.</returns>
    public async Task<CarDamageDetail> Add(CarDamageDetail carDamageDetail)
    {
        _context.CarDamageDetails.Add(carDamageDetail);
        await _context.SaveChangesAsync();
        return carDamageDetail;
    }

    /// <summary>
    /// Bir araç hasar detayını siler (işaretler).
    /// </summary>
    /// <param name="carDamageDetail">Silinecek araç hasar detayı.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(CarDamageDetail carDamageDetail)
    {
        _context.Entry(carDamageDetail).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm araç hasar detaylarını getirir.
    /// </summary>
    /// <returns>Tüm araç hasar detaylarının listesi.</returns>
    public async Task<IEnumerable<CarDamageDetail>> GetAllAsync()
    {
        return await _context.CarDamageDetails.ToListAsync();
    }

    /// <summary>
    /// Belirli bir araca ait hasar detaylarını getirir.
    /// </summary>
    /// <param name="carId">Aracın ID'si.</param>
    /// <returns>Belirli araca ait hasar detaylarının listesi.</returns>
    public async Task<IEnumerable<CarDamageDetail>> GetDetailsByCarIdAsync(int carId)
    {
        return await _context.CarDamageDetails
                             .Where(b => b.CarId == carId)
                             .ToListAsync();

    }

    /// <summary>
    /// Belirli bir araç hasar detayını ID ile getirir.
    /// </summary>
    /// <param name="carDamageDetailId">Getirilecek araç hasar detayının ID'si.</param>
    /// <returns>Belirli araç hasar detayının bilgileri.</returns>
    public async Task<CarDamageDetail> GetByIdAsync(int carDamageDetailId)
    {
        return await _context.CarDamageDetails.FirstOrDefaultAsync(b => b.DamageDetailId == carDamageDetailId);

    }

    /// <summary>
    /// Bir araç hasar detayını günceller.
    /// </summary>
    /// <param name="carDamageDetail">Güncellenecek araç hasar detayı.</param>
    /// <returns>Güncellenen araç hasar detayı.</returns>
    public async Task<CarDamageDetail> Update(CarDamageDetail carDamageDetail)
    {
        var existingCarDamageDetail = await _context.CarDamageDetails.FindAsync(carDamageDetail.DamageDetailId);

        if (existingCarDamageDetail != null)
        {
            _context.Entry(existingCarDamageDetail).CurrentValues.SetValues(carDamageDetail);

            await _context.SaveChangesAsync();
            return existingCarDamageDetail;
        }
        return null;
    }
}