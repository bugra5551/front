using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Müşteri CRUD operasyonları için depo sınıfı.
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CustomerRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CustomerRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir müşteri ekler.
    /// </summary>
    /// <param name="customer">Eklenecek müşteri.</param>
    /// <returns>Eklenen müşteri.</returns>
    public async Task<Customer> Add(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    /// <summary>
    /// Bir müşteriyi siler (işaretler).
    /// </summary>
    /// <param name="customer">Silinecek müşteri.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Customer customer)
    {
        var detectedReservation = await _context.Reservations
                                    .Where(r => r.CustomerId == customer.CustomerId && r.Status == "Rented")
                                    .FirstOrDefaultAsync();
        if (detectedReservation != null)
        {
            return false;
        }

        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm müşterileri getirir.
    /// </summary>
    /// <returns>Tüm müşterilerin listesi.</returns>
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers
                            // .Include(b => b.UserType)
                             .Include(b => b.Reservations)
                             .ToListAsync();
    }

    /// <summary>
    /// Belirli bir müşteriyi ID ile getirir.
    /// </summary>
    /// <param name="customerId">Getirilecek müşterinin ID'si.</param>
    /// <returns>Belirli müşterinin bilgileri.</returns>
    public async Task<Customer> GetByIdAsync(int customerId)
    {
        return await _context.Customers
            //.Include(b => b.UserType)
            .Include(b => b.Reservations)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    /// <summary>
    /// Bir müşteriyi günceller.
    /// </summary>
    /// <param name="customer">Güncellenecek müşteri.</param>
    /// <returns>Güncellenen müşteri.</returns>
    public async Task<Customer> Update(Customer customer)
    {
        var existingCustomer = await _context.Customers.FirstOrDefaultAsync(b => b.CustomerId == customer.CustomerId);
        if (existingCustomer != null)
        {
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();
            return existingCustomer;
        }
        return null;
    }
}
