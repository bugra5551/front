using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Rezervasyon işlemlerini yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    /// <summary>
    /// ReservationController sınıfının kurucusu.
    /// </summary>
    /// <param name="reservationService">Rezervasyon hizmeti.</param>
    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    /// <summary>
    /// Tüm rezervasyonları getirir.
    /// </summary>
    /// <returns>Rezervasyonlar listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var reservations = await _reservationService.GetAllAsync();
        return (reservations == null || !reservations.Any()) ? NotFound("Hiç rezervasyon bulunamadı.") : Ok(reservations);
    }

    /// <summary>
    /// Belirli bir rezervasyonu ID'ye göre getirir.
    /// </summary>
    /// <param name="reservationId">Rezervasyon ID'si.</param>
    /// <returns>Rezervasyon bilgileri.</returns>
    [HttpGet("GetByReservationId/{reservationId}")]
    public async Task<IActionResult> GetByIdAsync(int reservationId)
    {
        var reservation = await _reservationService.GetByIdAsync(reservationId);
        return reservation == null ? NotFound("Rezervasyon bulunamadı.") : Ok(reservation);
    }

    /// <summary>
    /// Belirli bir kullanıcıya ait rezervasyonları getirir.
    /// </summary>
    /// <param name="userId">Kullanıcı ID'si.</param>
    /// <returns>Kullanıcıya ait rezervasyonlar listesi.</returns>
    [HttpGet("GetReservationsByUserId/{userId}")]
    public async Task<IActionResult> GetReservationsByUserId(int userId)
    {
        var reservations = await _reservationService.GetReservationsByUserId(userId);
        return reservations == null ? NotFound("Rezervasyon bulunamadı.") : Ok(reservations);
    }

    /// <summary>
    /// Yeni bir rezervasyon ekler.
    /// </summary>
    /// <param name="reservationCreateUpdateDTO">Rezervasyon bilgileri.</param>
    /// <returns>Eklenen rezervasyon bilgileri.</returns>
    [HttpPost("Add")]
    public async Task<IActionResult> Add(ReservationCreateUpdateDTO reservationCreateUpdateDTO)
    {
        var addedReservation = await _reservationService.Add(reservationCreateUpdateDTO);
        return addedReservation == null ? BadRequest("Rezervasyon oluşturulurken bir hata oluştu.") : Ok(addedReservation);
    }

    /// <summary>
    /// Mevcut bir rezervasyonu günceller.
    /// </summary>
    /// <param name="reservationCreateUpdateDTO">Güncellenecek rezervasyon bilgileri.</param>
    /// <returns>Güncellenen rezervasyon bilgileri.</returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update(ReservationCreateUpdateDTO reservationCreateUpdateDTO)
    {
        var updatedReservation = await _reservationService.Update(reservationCreateUpdateDTO);
        return updatedReservation == null ? NotFound("Güncellenecek rezervasyon bulunamadı.") : Ok(updatedReservation);
    }

    /// <summary>
    /// Belirli bir rezervasyonu ID'ye göre siler.
    /// </summary>
    /// <param name="reservationId">Rezervasyon ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [HttpDelete("DeleteByReservationId/{reservationId}")]
    public async Task<IActionResult> Delete(int reservationId)
    {
        var isDeleted = await _reservationService.Delete(reservationId);
        return isDeleted ? Ok("Rezervasyon silindi.") : NotFound("Rezervasyon bulunamadı.");
    }
}
