using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Araç kiralama koşulları CRUD operasyonları için depo sınıfı.
/// </summary>
public class CarRentingConditionRepository : ICarRentingConditionRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CarRentingConditionRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CarRentingConditionRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir araç kiralama koşulu ekler.
    /// </summary>
    /// <param name="carRentingCondition">Eklenecek araç kiralama koşulu.</param>
    /// <returns>Eklenen araç kiralama koşulu.</returns>
    public async Task<CarRentingCondition> Add(CarRentingCondition carRentingCondition)
    {
        _context.CarRentingConditions.Add(carRentingCondition);
        await _context.SaveChangesAsync();
        return carRentingCondition;
    }

    /// <summary>
    /// Bir araç kiralama koşulunu siler (işaretler).
    /// </summary>
    /// <param name="carRentingCondition">Silinecek araç kiralama koşulu.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(CarRentingCondition carRentingCondition)
    {
        _context.Entry(carRentingCondition).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm araç kiralama koşullarını getirir.
    /// </summary>
    /// <returns>Tüm araç kiralama koşullarının listesi.</returns>
    public async Task<IEnumerable<CarRentingCondition>> GetAllAsync()
    {
        return await _context.CarRentingConditions.ToListAsync();
    }

    /// <summary>
    /// Belirli bir araç kiralama koşulunu ID ile getirir.
    /// </summary>
    /// <param name="carRentingConditionId">Getirilecek araç kiralama koşulunun ID'si.</param>
    /// <returns>Belirli araç kiralama koşulunun bilgileri.</returns>
    public async Task<CarRentingCondition> GetByIdAsync(int carRentingConditionId)
    {
        return await _context.CarRentingConditions.FirstOrDefaultAsync(b => b.CarRentingConditionId == carRentingConditionId);
    }

    /// <summary>
    /// Belirli bir araca ait kiralama koşullarını getirir.
    /// </summary>
    /// <param name="carId">Aracın ID'si.</param>
    /// <returns>Belirli araca ait kiralama koşullarının listesi.</returns>
    public async Task<IEnumerable<CarRentingCondition>> GetCarRentingConditionsByCarId(int carId)
    {
        return await _context.CarRentingConditions
            .Where(b => b.CarId == carId)
            .Include(b => b.Car)
            .Include(b => b.RentingCondition)
            .ToListAsync();
    }

    /// <summary>
    /// Bir araç kiralama koşulunu günceller.
    /// </summary>
    /// <param name="carRentingCondition">Güncellenecek araç kiralama koşulu.</param>
    /// <returns>Güncellenen araç kiralama koşulu.</returns>
    public async Task<CarRentingCondition> Update(CarRentingCondition carRentingCondition)
    {
        var existingCarRentingCondition = await _context.CarRentingConditions.FirstOrDefaultAsync(b => b.CarRentingConditionId == carRentingCondition.CarRentingConditionId);
        if (existingCarRentingCondition != null)
        {

            _context.Entry(existingCarRentingCondition).CurrentValues.SetValues(carRentingCondition);
            await _context.SaveChangesAsync();
            return existingCarRentingCondition;
        }
        return null;
    }
}