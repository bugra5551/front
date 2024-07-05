using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Kullanıcı türü CRUD operasyonları için depo sınıfı.
/// </summary>
public class UserTypeRepository : IUserTypeRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// UserTypeRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public UserTypeRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir kullanıcı türü ekler.
    /// </summary>
    /// <param name="userType">Eklenecek kullanıcı türü.</param>
    /// <returns>Eklenen kullanıcı türü.</returns>
    public async Task<UserType> Add(UserType userType)
    {
        _context.UserTypes.Add(userType);
        await _context.SaveChangesAsync();
        return userType;
    }

    /// <summary>
    /// Bir kullanıcı türünü siler.
    /// </summary>
    /// <param name="userType">Silinecek kullanıcı türü.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(UserType userType)
    {
        var detectedUserType = await _context.UserTypes
                            .Where(r => r.UserTypeId == userType.UserTypeId && r.Users != null)
                            .FirstOrDefaultAsync();

        if (detectedUserType != null)
        {
            return false;
        }

        _context.Entry(userType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm kullanıcı türlerini getirir.
    /// </summary>
    /// <returns>Tüm kullanıcı türlerinin listesi.</returns>
    public async Task<IEnumerable<UserType>> GetAllAsync()
    {
        return await _context.UserTypes
                     .ToListAsync();
    }

    /// <summary>
    /// Belirli bir kullanıcı türünü ID ile getirir.
    /// </summary>
    /// <param name="userTypeId">Getirilecek kullanıcı türünün ID'si.</param>
    /// <returns>Belirli kullanıcı türünün bilgileri.</returns>
    public async Task<UserType> GetByIdAsync(int userTypeId)
    {
        return await _context.UserTypes
              .FirstOrDefaultAsync(c => c.UserTypeId == userTypeId);
    }

    /// <summary>
    /// Bir kullanıcı türünü günceller.
    /// </summary>
    /// <param name="userType">Güncellenecek kullanıcı türü.</param>
    /// <returns>Güncellenen kullanıcı türü.</returns>
    public async Task<UserType> Update(UserType userType)
    {
        var existingUserType = await _context.UserTypes.FirstOrDefaultAsync(b => b.UserTypeId == userType.UserTypeId);
        if (existingUserType != null)
        {
            _context.Entry(existingUserType).CurrentValues.SetValues(userType);
            await _context.SaveChangesAsync();
            return existingUserType;
        }
        return null;
    }
}