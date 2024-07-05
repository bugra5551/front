using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Araç CRUD operasyonları için depo sınıfı.
/// </summary>
public class CarRepository : ICarRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CarRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CarRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir araç ekler.
    /// </summary>
    /// <param name="car">Eklenecek araç.</param>
    /// <returns>Eklenen araç.</returns>
    public async Task<Car> Add(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }

    /// <summary>
    /// Bir aracı siler (işaretler).
    /// </summary>
    /// <param name="car">Silinecek araç.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Car car)
    {

        var detectedReservation = await _context.Reservations
                                    .Where(r => r.CarId == car.CarId && r.Status == "Rented")
                                    .FirstOrDefaultAsync();

        if (detectedReservation != null)
        {
            return false;
        }
        _context.Entry(car).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm araçları getirir.
    /// </summary>
    /// <returns>Tüm araçların listesi.</returns>
    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await _context.Cars
            .Include(b => b.CarClass)
            .Include(b => b.Location)
            .Include(b => b.Model)
               .ThenInclude(t => t.Brand)
            .Include(b => b.Reservations)
            .Include(b => b.CarSpecifications)
                .ThenInclude(s => s.SpecificationDescription)
            .Include(b => b.CarRentingConditions)
                .ThenInclude(c => c.RentingCondition)
            //.Include(b => b.CarImages)
            .Include(b => b.CarDamageDetails).ToListAsync();
    }

    /// <summary>
    /// Belirli bir aracı ID ile getirir.
    /// </summary>
    /// <param name="carId">Getirilecek aracın ID'si.</param>
    /// <returns>Belirli aracın bilgileri.</returns>
    public async Task<Car> GetByIdAsync(int carId)
    {
        var car = await _context.Cars
            .Include(b => b.CarClass)
            .Include(b => b.Location)
            .Include(b => b.Model)
               .ThenInclude(t => t.Brand)
            .Include(b => b.Reservations)
            .Include(b => b.CarSpecifications)
                .ThenInclude(s => s.SpecificationDescription)
            .Include(b => b.CarRentingConditions)
                .ThenInclude(c => c.RentingCondition)
            //.Include(b => b.CarImages)
            .Include(b => b.CarDamageDetails).FirstOrDefaultAsync(b => b.CarId == carId);
        if (car == null) return null;
        return car;
    }

    /// <summary>
    /// Bir aracı günceller.
    /// </summary>
    /// <param name="car">Güncellenecek araç.</param>
    /// <returns>Güncellenen araç.</returns>
    public async Task<Car> Update(Car car)
    {
        var existingCar = await _context.Cars.FirstOrDefaultAsync(b => b.CarId == car.CarId);
        if (existingCar != null)
        {

            _context.Entry(existingCar).CurrentValues.SetValues(car);
            await _context.SaveChangesAsync();
            return existingCar;
        }
        return null;
    }
}