using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Araç özellikleri CRUD operasyonları için depo sınıfı.
/// </summary>
public class CarSpecificationRepository : ICarSpecificationRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CarSpecificationRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CarSpecificationRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir araç özelliği ekler.
    /// </summary>
    /// <param name="carSpecification">Eklenecek araç özelliği.</param>
    /// <returns>Eklenen araç özelliği.</returns>
    public async Task<CarSpecification> Add(CarSpecification carSpecification)
    {
        _context.CarSpecifications.Add(carSpecification);
        await _context.SaveChangesAsync();
        return carSpecification;
    }

    /// <summary>
    /// Bir araç özelliğini siler (işaretler).
    /// </summary>
    /// <param name="carSpecification">Silinecek araç özelliği.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(CarSpecification carSpecification)
    {

        var detectedCar = await _context.Cars
                                    .Where(c => c.CarId == carSpecification.CarId)
                                    .FirstOrDefaultAsync();

        if (detectedCar != null)
        {
            return false;
        }
        _context.Entry(carSpecification).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm araç özelliklerini getirir.
    /// </summary>
    /// <returns>Tüm araç özelliklerinin listesi.</returns>
    public async Task<IEnumerable<CarSpecification>> GetAllAsync()
    {
        return await _context.CarSpecifications
            .Include(b => b.SpecificationDescription).ToListAsync();
    }

    /// <summary>
    /// Belirli bir araç özelliğini ID ile getirir.
    /// </summary>
    /// <param name="carSpecificationId">Getirilecek araç özelliğinin ID'si.</param>
    /// <returns>Belirli araç özelliğinin bilgileri.</returns>
    public async Task<CarSpecification> GetByIdAsync(int carSpecificationId)
    {
        var carSpecification = await _context.CarSpecifications
            .Include(c => c.Car)
            .Include(b => b.SpecificationDescription)
            .FirstOrDefaultAsync(b => b.CarSpecificationId == carSpecificationId);
        if (carSpecification == null) return null;
        return carSpecification;
    }

    /// <summary>
    /// Bir araç özelliğini günceller.
    /// </summary>
    /// <param name="carSpecification">Güncellenecek araç özelliği.</param>
    /// <returns>Güncellenen araç özelliği.</returns>
    public async Task<CarSpecification> Update(CarSpecification carSpecification)
    {
        var existingCarSpecification = await _context.CarSpecifications.FirstOrDefaultAsync(b => b.CarSpecificationId == carSpecification.CarSpecificationId);
        if (existingCarSpecification != null)
        {
            _context.Entry(existingCarSpecification).CurrentValues.SetValues(carSpecification);
            await _context.SaveChangesAsync();
            return existingCarSpecification;
        }
        return null;
    }
}