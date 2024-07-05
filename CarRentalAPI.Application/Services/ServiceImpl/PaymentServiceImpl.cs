using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Ödeme CRUD operasyonları için servis sınıfı.
/// </summary>
public class PaymentServiceImpl : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// PaymentServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="paymentRepository">Ödeme deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public PaymentServiceImpl(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir ödeme ekler.
    /// </summary>
    /// <param name="paymentCreateUpdateDTO">Eklenecek ödeme bilgileri.</param>
    /// <returns>Eklenen ödeme bilgileri.</returns>
    public async Task<PaymentCreateUpdateDTO> Add(PaymentCreateUpdateDTO paymentCreateUpdateDTO)
    {
        paymentCreateUpdateDTO.isDeleted = false;
        var payment = _mapper.Map<Payment>(paymentCreateUpdateDTO);
        var addedPayment = await _paymentRepository.Add(payment);
        return _mapper.Map<PaymentCreateUpdateDTO>(addedPayment);
    }

    /// <summary>
    /// Bir ödemeyi siler.
    /// </summary>
    /// <param name="paymentId">Silinecek ödemenin ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int paymentId)
    {
        var existingPayment = await _paymentRepository.GetByIdAsync(paymentId);
        if (existingPayment != null)
        {
            existingPayment.isDeleted = true;
            return await _paymentRepository.Delete(existingPayment);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Tüm ödemeleri getirir.
    /// </summary>
    /// <returns>Tüm ödemelerin listesi.</returns>
    public async Task<IEnumerable<PaymentDTO>> GetAllAsync()
    {
        var payments = await _paymentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PaymentDTO>>(payments);
    }

    /// <summary>
    /// Belirli bir ödemeyi ID ile getirir.
    /// </summary>
    /// <param name="paymentId">Getirilecek ödemenin ID'si.</param>
    /// <returns>Belirli ödemenin bilgileri.</returns>
    public async Task<PaymentDTO> GetByIdAsync(int paymentId)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        return _mapper.Map<PaymentDTO>(payment);
    }

    /// <summary>
    /// Bir ödemeyi günceller.
    /// </summary>
    /// <param name="paymentCreateUpdateDTO">Güncellenecek ödeme bilgileri.</param>
    /// <returns>Güncellenen ödeme bilgileri.</returns>
    public async Task<PaymentCreateUpdateDTO> Update(PaymentCreateUpdateDTO paymentCreateUpdateDTO)
    {
        var payment = _mapper.Map<Payment>(paymentCreateUpdateDTO);

        var updatedPayment = await _paymentRepository.Update(payment);
        if (updatedPayment != null)
        {
            return _mapper.Map<PaymentCreateUpdateDTO>(updatedPayment);
        }
        return null;
    }
}