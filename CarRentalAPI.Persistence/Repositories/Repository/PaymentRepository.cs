using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Ödeme CRUD operasyonları için depo sınıfı.
/// </summary>
public class PaymentRepository : IPaymentRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// PaymentRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public PaymentRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir ödeme ekler.
    /// </summary>
    /// <param name="payment">Eklenecek ödeme.</param>
    /// <returns>Eklenen ödeme.</returns>
    public async Task<Payment> Add(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    /// <summary>
    /// Bir ödemeyi siler (işaretler).
    /// </summary>
    /// <param name="payment">Silinecek ödeme.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Payment payment)
    {
        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Tüm ödemeleri getirir.
    /// </summary>
    /// <returns>Tüm ödemelerin listesi.</returns>
    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    /// <summary>
    /// Belirli bir ödemeyi ID ile getirir.
    /// </summary>
    /// <param name="paymentId">Getirilecek ödemenin ID'si.</param>
    /// <returns>Belirli ödemenin bilgileri.</returns>
    public async Task<Payment> GetByIdAsync(int paymentId)
    {
        return await _context.Payments
                             .FirstOrDefaultAsync(b => b.PaymentId == paymentId);
    }

    /// <summary>
    /// Bir ödemeyi günceller.
    /// </summary>
    /// <param name="payment">Güncellenecek ödeme.</param>
    /// <returns>Güncellenen ödeme.</returns>
    public async Task<Payment> Update(Payment payment)
    {
        var existingPayment = await _context.Payments.FindAsync(payment.PaymentId);
        if (existingPayment != null)
        {
            _context.Entry(existingPayment).CurrentValues.SetValues(payment);
            await _context.SaveChangesAsync();
            return existingPayment;
        }
        return null;
    }
}