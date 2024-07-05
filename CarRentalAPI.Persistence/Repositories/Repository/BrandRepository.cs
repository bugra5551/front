using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Marka CRUD operasyonları için depo sınıfı.
/// </summary>
public class BrandRepository : IBrandRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// BrandRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public BrandRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir marka ekler.
    /// </summary>
    /// <param name="brand">Eklenecek marka.</param>
    /// <returns>Eklenen marka.</returns>
    public async Task<Brand> Add(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    /// <summary>
    /// Bir markayı siler (işaretler).
    /// </summary>
    /// <param name="brand">Silinecek marka.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Brand brand)
    {
        _context.Entry(brand).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm markaları getirir.
    /// </summary>
    /// <returns>Tüm markaların listesi.</returns>
    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    /// <summary>
    /// Belirli bir markayı ID ile getirir.
    /// </summary>
    /// <param name="brandId">Getirilecek markanın ID'si.</param>
    /// <returns>Belirli markanın bilgileri.</returns>
    public async Task<Brand> GetByIdAsync(int brandId)
    {
        return await _context.Brands.Include(b => b.Models).FirstOrDefaultAsync(b => b.BrandId == brandId);
    }

    /// <summary>
    /// Bir markayı günceller.
    /// </summary>
    /// <param name="brand">Güncellenecek marka.</param>
    /// <returns>Güncellenen marka.</returns>
    public async Task<Brand> Update(Brand brand)
    {
        var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandId == brand.BrandId);
        if (existingBrand != null)
        {

            _context.Entry(existingBrand).CurrentValues.SetValues(brand);
            await _context.SaveChangesAsync();
            return existingBrand;
        }
        return null;
    }
}