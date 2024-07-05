using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araba kiralama şartları ile ilgili işlemleri yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CarRentingConditionController : ControllerBase
{
    private readonly ICarRentingConditionService _carRentingConditionService;

    /// <summary>
    /// CarRentingConditionController sınıfının kurucusu.
    /// </summary>
    /// <param name="carRentingConditionService">Araba kiralama şartı hizmeti.</param>
    public CarRentingConditionController(ICarRentingConditionService carRentingConditionService)
    {
        _carRentingConditionService = carRentingConditionService;
    }

    /// <summary>
    /// Tüm araba kiralama şartlarını getirir.
    /// </summary>
    /// <returns>Araba kiralama şartları listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCars()
    {
        var carRentingConditions = await _carRentingConditionService.GetAllAsync();
        return (carRentingConditions == null || !carRentingConditions.Any()) ? NotFound("Hiç araba kiralama şartı bulunamadı.") : Ok(carRentingConditions);
    }

    /// <summary>
    /// Belirli bir araba kiralama şartı ID'sine göre araba kiralama şartını getirir.
    /// </summary>
    /// <param name="carRentingConditionId">Araba kiralama şartı ID'si.</param>
    /// <returns>Araba kiralama şartı bilgileri.</returns>
    [HttpGet("GetByCarRentingConditionId/{carRentingConditionId}")]
    public async Task<IActionResult> GetByIdAsync(int carRentingConditionId)
    {
        var carRentingCondition = await _carRentingConditionService.GetByIdAsync(carRentingConditionId);
        return carRentingCondition == null ? NotFound("Araba kiralama şartı bulunamadı.") : Ok(carRentingCondition);
    }

    /// <summary>
    /// Yeni bir araba kiralama şartı ekler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carRentingConditionCreateUpdate">Eklenecek araba kiralama şartı bilgileri.</param>
    /// <returns>Eklenen araba kiralama şartı bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(CarRentingConditionCreateUpdateDTO carRentingConditionCreateUpdate)
    {
        var addedCarRentingCondition = await _carRentingConditionService.Add(carRentingConditionCreateUpdate);
        return addedCarRentingCondition == null ? BadRequest("Araba kiralama şartı eklenirken bir hata oluştu.") : Ok(addedCarRentingCondition);
    }

    /// <summary>
    /// Mevcut bir araba kiralama şartını günceller. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carRentingConditionCreateUpdate">Güncellenmek istenen araba kiralama şartı bilgileri.</param>
    /// <returns>Güncellenen araba kiralama şartı bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(CarRentingConditionCreateUpdateDTO carRentingConditionCreateUpdate)
    {
        var updatedCarRentingCondition = await _carRentingConditionService.Update(carRentingConditionCreateUpdate);
        return updatedCarRentingCondition == null ? NotFound("Güncellenecek araba kiralama şartı bulunamadı.") : Ok(updatedCarRentingCondition);
    }

    /// <summary>
    /// Belirli bir araba kiralama şartı ID'sine göre araba kiralama şartını siler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carRentingConditionId">Silinmek istenen araba kiralama şartı ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByCarId/{carRentingConditionId}")]
    public async Task<IActionResult> Delete(int carRentingConditionId)
    {
        var isDeleted = await _carRentingConditionService.Delete(carRentingConditionId);
        return isDeleted ? Ok("Araba kiralama şartı silindi.") : NotFound("Araba kiralama şartı bulunamadı.");
    }

    /// <summary>
    /// Belirli bir araba ID'sine göre araba kiralama şartlarını getirir.
    /// </summary>
    /// <param name="carId">Araba ID'si.</param>
    /// <returns>Araba kiralama şartları listesi.</returns>
    [HttpGet("GetCarRentingConditionsByCarId/{carId}")]
    public async Task<IActionResult> GetCarRentingConditionsByCarId(int carId)
    {
        var carRentingConditions = await _carRentingConditionService.GetCarRentingConditionsByCarIdAsync(carId);
        if (carRentingConditions == null) return NotFound();
        return Ok(carRentingConditions);
    }
}
