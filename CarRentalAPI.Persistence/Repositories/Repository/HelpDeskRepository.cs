using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Yardım masası mesajları CRUD operasyonları için depo sınıfı.
/// </summary>
public class HelpDeskRepository : IHelpDeskRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// HelpDeskRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public HelpDeskRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Belirli bir araç ve kullanıcı ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <param name="userId">Kullanıcı ID'si.</param>
    /// <returns>Belirli araç ve kullanıcıya ait mesajların listesi.</returns>
    public async Task<IEnumerable<HelpDesk>> GetMessagesByCarIdAndUserId(int carId, int userId)
    {
        return await _context.HelpDesks.Where(m => m.CarId == carId && m.UserId == userId)
            .Include(m => m.User).OrderByDescending(m => m.MessageDate).ToListAsync();
    }

    /// <summary>
    /// Belirli bir araç ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli araca ait mesajların listesi.</returns>
    public async Task<IEnumerable<HelpDesk>> GetMessagesByCarId(int carId)
    {
        return await _context.HelpDesks.Where(m => m.CarId == carId).Include(m => m.User).OrderByDescending(m => m.MessageDate).ToListAsync();
    }

    /// <summary>
    /// Yeni bir yardım masası mesajı ekler.
    /// </summary>
    /// <param name="message">Eklenecek mesaj.</param>
    public async Task<HelpDesk> AddMessage(HelpDesk message)
    {
        _context.HelpDesks.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }
}
