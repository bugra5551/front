using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Ödeme işlemlerini yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    /// <summary>
    /// PaymentController sınıfının kurucusu.
    /// </summary>
    /// <param name="paymentService">Ödeme hizmeti.</param>
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    /// <summary>
    /// Tüm ödeme bilgilerini getirir.
    /// </summary>
    /// <returns>Ödeme bilgileri listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllPaymnets()
    {
        var payments = await _paymentService.GetAllAsync();
        return (payments == null || !payments.Any()) ? NotFound("Hiç ödeme bilgisi bulunamadı.") : Ok(payments);
    }

    /// <summary>
    /// Belirli bir ödeme bilgisi ID'ye göre getirir.
    /// </summary>
    /// <param name="paymentId">Ödeme ID'si.</param>
    /// <returns>Ödeme bilgisi.</returns>
    [HttpGet("GetByPaymentId/{paymentId}")]
    public async Task<IActionResult> GetByIdAsync(int paymentId)
    {
        var payment = await _paymentService.GetByIdAsync(paymentId);
        return payment == null ? NotFound("Ödeme bilgisi bulunamadı.") : Ok(payment);
    }

    /// <summary>
    /// Yeni bir ödeme ekler.
    /// </summary>
    /// <param name="paymentCreateUpdateDTO">Ödeme bilgileri.</param>
    /// <returns>Eklenen ödeme bilgileri.</returns>
    [HttpPost("Add")]
    public async Task<IActionResult> Add(PaymentCreateUpdateDTO paymentCreateUpdateDTO)
    {
        var addedPayment = await _paymentService.Add(paymentCreateUpdateDTO);
        return addedPayment == null ? BadRequest("Ödeme eklenirken bir hata oluştu.") : Ok(addedPayment);
    }

    /// <summary>
    /// Mevcut bir ödemeyi günceller.
    /// </summary>
    /// <param name="paymentCreateUpdateDTO">Güncellenecek ödeme bilgileri.</param>
    /// <returns>Güncellenen ödeme bilgileri.</returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update(PaymentCreateUpdateDTO paymentCreateUpdateDTO)
    {
        var updatedPayment = await _paymentService.Update(paymentCreateUpdateDTO);
        return updatedPayment == null ? NotFound("Güncellenecek ödeme bulunamadı.") : Ok(updatedPayment);
    }

    /// <summary>
    /// Belirli bir ödemeyi ID'ye göre siler.
    /// </summary>
    /// <param name="paymentId">Ödeme ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [HttpDelete("DeleteByPaymentId/{paymentId}")]
    public async Task<IActionResult> Delete(int paymentId)
    {
        var isDeleted = await _paymentService.Delete(paymentId);
        return isDeleted ? Ok("Ödeme silindi.") : NotFound("Ödeme bilgisi bulunamadı.");
    }
}