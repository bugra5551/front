using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Özellik açıklamaları CRUD operasyonları için depo sınıfı.
/// </summary>
public class SpecificationDescriptionRepository : ISpecificationDescriptionRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// SpecificationDescriptionRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public SpecificationDescriptionRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir özellik açıklaması ekler.
    /// </summary>
    /// <param name="specificationDescription">Eklenecek özellik açıklaması.</param>
    /// <returns>Eklenen özellik açıklaması.</returns>
    public async Task<SpecificationDescription> Add(SpecificationDescription specificationDescription)
    {
        _context.SpecificationsDescriptions.Add(specificationDescription);
        await _context.SaveChangesAsync();
        return specificationDescription;
    }

    /// <summary>
    /// Bir özellik açıklamasını siler (işaretler).
    /// </summary>
    /// <param name="specificationDescription">Silinecek özellik açıklaması.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(SpecificationDescription specificationDescription)
    {
        var detectedCarSpecification = await _context.CarSpecifications
                                         .Where(c => c.CarSpecificationId == specificationDescription.SpecificationDescriptionId && c.isDeleted == true)
                                         .FirstOrDefaultAsync();

        if (detectedCarSpecification != null)
        {
            return false;
        }

        _context.Entry(specificationDescription).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;

    }

    /// <summary>
    /// Tüm özellik açıklamalarını getirir.
    /// </summary>
    /// <returns>Tüm özellik açıklamalarının listesi.</returns>
    public async Task<IEnumerable<SpecificationDescription>> GetAllAsync()
    {
        return await _context.SpecificationsDescriptions.ToListAsync();
    }

    /// <summary>
    /// Belirli bir özellik açıklamasını ID ile getirir.
    /// </summary>
    /// <param name="specificationDescriptionId">Getirilecek özellik açıklamasının ID'si.</param>
    /// <returns>Belirli özellik açıklamasının bilgileri.</returns>
    public async Task<SpecificationDescription> GetByIdAsync(int specificationDescriptionId)
    {
        var specificationDescription = await _context.SpecificationsDescriptions
            //.Include(b => b.CarSpecifications)
            //.ThenInclude(b => b.Car)
            .FirstOrDefaultAsync(b => b.SpecificationDescriptionId == specificationDescriptionId);

        if (specificationDescription == null) return null;
        return specificationDescription;
    }

    /// <summary>
    /// Bir özellik açıklamasını günceller.
    /// </summary>
    /// <param name="specificationDescription">Güncellenecek özellik açıklaması.</param>
    /// <returns>Güncellenen özellik açıklaması.</returns>
    public async Task<SpecificationDescription> Update(SpecificationDescription specificationDescription)
    {
        var existingSpecificationDescription = await _context.SpecificationsDescriptions.FirstOrDefaultAsync(b => b.SpecificationDescriptionId == specificationDescription.SpecificationDescriptionId);
        
        if (existingSpecificationDescription != null)
        {
            _context.Entry(existingSpecificationDescription).CurrentValues.SetValues(specificationDescription);
            await _context.SaveChangesAsync();
            return specificationDescription;
        }
        return null;
    }
}