using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Kiralama koşulları CRUD operasyonları için depo sınıfı.
/// </summary>
public class RentingConditionRepository : IRentingConditionRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// RentingConditionRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public RentingConditionRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir kiralama koşulu ekler.
    /// </summary>
    /// <param name="rentingCondition">Eklenecek kiralama koşulu.</param>
    /// <returns>Eklenen kiralama koşulu.</returns>
    public async Task<RentingCondition> Add(RentingCondition rentingCondition)
    {
        _context.RentingConditions.Add(rentingCondition);
        await _context.SaveChangesAsync();
        return rentingCondition;
    }

    /// <summary>
    /// Bir kiralama koşulunu siler (işaretler).
    /// </summary>
    /// <param name="rentingCondition">Silinecek kiralama koşulu.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(RentingCondition rentingCondition)
    {
        var detectedCarRentingCondition = await _context.CarRentingConditions
            .Where(cr => cr.RentingConditionId == rentingCondition.RentingConditionId && cr.isDeleted == false)
            .FirstOrDefaultAsync();

        if (detectedCarRentingCondition != null)
        {
            return false;
        }
        _context.Entry(rentingCondition).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm kiralama koşullarını getirir.
    /// </summary>
    /// <returns>Tüm kiralama koşullarının listesi.</returns>
    public async Task<IEnumerable<RentingCondition>> GetAllAsync()
    {
        return await _context.RentingConditions.ToListAsync();
    }

    /// <summary>
    /// Belirli bir kiralama koşulunu ID ile getirir.
    /// </summary>
    /// <param name="rentingConditionId">Getirilecek kiralama koşulunun ID'si.</param>
    /// <returns>Belirli kiralama koşulunun bilgileri.</returns>
    public async Task<RentingCondition> GetByIdAsync(int rentingConditionId)
    {
        return await _context.RentingConditions.FirstOrDefaultAsync(b => b.RentingConditionId == rentingConditionId);

    }

    /// <summary>
    /// Bir kiralama koşulunu günceller.
    /// </summary>
    /// <param name="rentingCondition">Güncellenecek kiralama koşulu.</param>
    /// <returns>Güncellenen kiralama koşulu.</returns>
    public async Task<RentingCondition> Update(RentingCondition rentingCondition)
    {
        var existingRentingCondition = await _context.RentingConditions.FindAsync(rentingCondition.RentingConditionId);
        if (existingRentingCondition != null)
        {
            _context.Entry(existingRentingCondition).CurrentValues.SetValues(rentingCondition);
            await _context.SaveChangesAsync();
            return existingRentingCondition;
        }
        return null;
    }
}
