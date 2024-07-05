using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Lokasyon CRUD operasyonları için depo sınıfı.
/// </summary>
public class LocationRepository : ILocationRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// LocationRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public LocationRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir lokasyon ekler.
    /// </summary>
    /// <param name="location">Eklenecek lokasyon.</param>
    /// <returns>Eklenen lokasyon.</returns>
    public async Task<Location> Add(Location location)
    {
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        return location;
    }

    /// <summary>
    /// Bir lokasyonu siler (işaretler).
    /// </summary>
    /// <param name="location">Silinecek lokasyon.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Location location)
    {
        var existingLocation = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == location.LocationId);
        if (existingLocation != null)
        {

            _context.Entry(existingLocation).CurrentValues.SetValues(location);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Tüm lokasyonları getirir.
    /// </summary>
    /// <returns>Tüm lokasyonların listesi.</returns>
    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    /// <summary>
    /// Belirli bir lokasyonu ID ile getirir.
    /// </summary>
    /// <param name="locationId">Getirilecek lokasyonun ID'si.</param>
    /// <returns>Belirli lokasyonun bilgileri.</returns>
    public async Task<Location> GetByIdAsync(int locationId)
    {
        return await _context.Locations
                         //.Include(c => c.Cars)
                         //.Include(t => t.PickUpReservations)
                         //.Include(t => t.DropOffReservations)
                         .FirstOrDefaultAsync(b => b.LocationId == locationId);
    }

    /// <summary>
    /// Bir lokasyonu günceller.
    /// </summary>
    /// <param name="location">Güncellenecek lokasyon.</param>
    /// <returns>Güncellenen lokasyon.</returns>
    public async Task<Location> Update(Location location)
    {
        var existingLocation = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == location.LocationId);
        if (existingLocation != null)
        {

            _context.Entry(existingLocation).CurrentValues.SetValues(location);
            await _context.SaveChangesAsync();
            return existingLocation;
        }
        return null;
    }
}
