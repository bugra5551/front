using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Model CRUD operasyonları için depo sınıfı.
/// </summary>
public class ModelRepository : IModelRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// ModelRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public ModelRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir model ekler.
    /// </summary>
    /// <param name="model">Eklenecek model.</param>
    /// <returns>Eklenen model.</returns>
    public async Task<Model> Add(Model model)
    {
        _context.Models.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    /// <summary>
    /// Bir modeli siler (işaretler).
    /// </summary>
    /// <param name="model">Silinecek model.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Model model)
    {
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm modelleri getirir.
    /// </summary>
    /// <returns>Tüm modellerin listesi.</returns>
    public async Task<IEnumerable<Model>> GetAllAsync()
    {
        return await _context.Models.ToListAsync();
    }

    /// <summary>
    /// Belirli bir modeli ID ile getirir.
    /// </summary>
    /// <param name="modelId">Getirilecek modelin ID'si.</param>
    /// <returns>Belirli modelin bilgileri.</returns>
    public async Task<Model> GetByIdAsync(int modelId)
    {
        return await _context.Models.Include(b => b.Cars)
                              .FirstOrDefaultAsync(b => b.ModelId == modelId);
    }

    /// <summary>
    /// Marka ID'sine göre modelleri getirir.
    /// </summary>
    /// <param name="brandId">Marka ID'si.</param>
    /// <returns>Belirli bir markaya ait modellerin listesi.</returns>
    public async Task<IEnumerable<Model>> GetModelsByBrandIdAsync(int brandId)
    {
        return await _context.Models.Where(m => m.BrandId == brandId).ToListAsync();
    }

    /// <summary>
    /// Bir modeli günceller.
    /// </summary>
    /// <param name="model">Güncellenecek model.</param>
    /// <returns>Güncellenen model.</returns>
    public async Task<Model> Update(Model model)
    {
        var existingModel = await _context.Models.FirstOrDefaultAsync(b => b.ModelId == model.ModelId);
        if (existingModel != null)
        {
            _context.Entry(existingModel).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return existingModel;
        }
        return null;
    }
}
