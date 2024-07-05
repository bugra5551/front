using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Araç sınıfı CRUD operasyonları için depo sınıfı.
/// </summary>
public class CarClassRepository : ICarClassRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CarClassRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CarClassRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir araç sınıfı ekler.
    /// </summary>
    /// <param name="carClass">Eklenecek araç sınıfı.</param>
    /// <returns>Eklenen araç sınıfı.</returns>
    public async Task<CarClass> Add(CarClass carClass)
    {
        _context.CarClasses.Add(carClass);
        await _context.SaveChangesAsync();
        return carClass;
    }

    /// <summary>
    /// Bir araç sınıfını siler (işaretler).
    /// </summary>
    /// <param name="carClass">Silinecek araç sınıfı.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(CarClass carClass)
    {
        _context.Entry(carClass).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm araç sınıflarını getirir.
    /// </summary>
    /// <returns>Tüm araç sınıflarının listesi.</returns>
    public async Task<IEnumerable<CarClass>> GetAllAsync()
    {
        return await _context.CarClasses.ToListAsync();
    }

    /// <summary>
    /// Belirli bir araç sınıfını ID ile getirir.
    /// </summary>
    /// <param name="carClassId">Getirilecek araç sınıfının ID'si.</param>
    /// <returns>Belirli araç sınıfının bilgileri.</returns>
    public async Task<CarClass> GetByIdAsync(int carClassId)
    {
        return await _context.CarClasses.FirstOrDefaultAsync(b => b.CarClassId == carClassId);

        //.Include(b => b.Cars)

    }
    /// <summary>
    /// Bir araç sınıfını günceller.
    /// </summary>
    /// <param name="carClass">Güncellenecek araç sınıfı.</param>
    /// <returns>Güncellenen araç sınıfı.</returns>
    public async Task<CarClass> Update(CarClass carClass)
    {
        var existingCarClass = await _context.CarClasses.FindAsync(carClass.CarClassId);
        if (existingCarClass != null)
        {
            _context.Entry(existingCarClass).CurrentValues.SetValues(carClass);
            await _context.SaveChangesAsync();
            return existingCarClass;
        }
        return null;
    }
}