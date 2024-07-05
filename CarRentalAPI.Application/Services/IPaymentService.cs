using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Ödeme CRUD operasyonları için servis arayüzü.
/// </summary>
public interface IPaymentService : IService<PaymentCreateUpdateDTO, PaymentDTO> { }
