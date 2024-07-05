using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Rezervasyon CRUD operasyonları için depo sınıfı.
/// </summary>
public class ReservationRepository : IReservationRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// ReservationRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public ReservationRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir rezervasyon ekler.
    /// </summary>
    /// <param name="reservation">Eklenecek rezervasyon.</param>
    /// <returns>Eklenen rezervasyon.</returns>
    public async Task<Reservation> Add(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    /// <summary>
    /// Bir rezervasyonu siler (işaretler).
    /// </summary>
    /// <param name="reservation">Silinecek rezervasyon.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Reservation reservation)
    {

        var detectedReservation = await _context.Reservations
                                    .Where(r => r.ReservationId == reservation.ReservationId && r.Customer != null)
                                    .FirstOrDefaultAsync();

        if (detectedReservation != null)
        {
            return false;
        }
        _context.Entry(reservation).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm rezervasyonları getirir.
    /// </summary>
    /// <returns>Tüm rezervasyonların listesi.</returns>
    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    /// <summary>
    /// Belirli bir rezervasyonu ID ile getirir.
    /// </summary>
    /// <param name="reservationId">Getirilecek rezervasyonun ID'si.</param>
    /// <returns>Belirli rezervasyonun bilgileri.</returns>
    public async Task<Reservation> GetByIdAsync(int reservationId)
    {
        return await _context.Reservations
                             .Include(c => c.Customer)
                             .Include(c => c.Car)
                             .Include(p => p.Payment)
                             .Include(l => l.SelectedPickUpLocation)
                             //.Where(l=>l.PickUpLocation==l.SelectedPickUpLocation.LocationId)
                             .Include(l => l.SelectedDropOffLocation)
                             //.Where(l => l.DropOffLocation == l.SelectedDropOffLocation.LocationId)
                             .FirstOrDefaultAsync(b => b.ReservationId == reservationId);
    }

    /// <summary>
    /// Kullanıcı ID'sine göre rezervasyonları getirir.
    /// </summary>
    /// <param name="userId">Kullanıcı ID'si.</param>
    /// <returns>Kullanıcıya ait rezervasyonların listesi.</returns>
    public async Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId)
    {
        return await _context.Reservations.Where(b => b.CustomerId == userId)
                             .Include(c => c.Car).ThenInclude(c => c.Model).ThenInclude(c => c.Brand)
                             .Include(p => p.Payment)
                             .Include(l => l.SelectedPickUpLocation)
                             //.Where(l=>l.PickUpLocation==l.SelectedPickUpLocation.LocationId)
                             .Include(l => l.SelectedDropOffLocation)
                             .ToListAsync();
    }

    /// <summary>
    /// Bir rezervasyonu günceller.
    /// </summary>
    /// <param name="reservation">Güncellenecek rezervasyon.</param>
    /// <returns>Güncellenen rezervasyon.</returns>
    public async Task<Reservation> Update(Reservation reservation)
    {

        var existingReservation = await _context.Reservations.FirstOrDefaultAsync(b => b.ReservationId == reservation.ReservationId);
        if (existingReservation != null)
        {

            _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
            await _context.SaveChangesAsync();
            return existingReservation;
        }
        return null;
    }
}
