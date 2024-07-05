using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Kiralama durumu işlemlerini yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class RentingConditionController : ControllerBase
{
    private readonly IRentingConditionService _rentingConditionService;

    /// <summary>
    /// RentingConditionController sınıfının kurucusu.
    /// </summary>
    /// <param name="rentingConditionService">Kiralama durumu hizmeti.</param>
    public RentingConditionController(IRentingConditionService rentingConditionService)
    {
        _rentingConditionService = rentingConditionService;
    }

    /// <summary>
    /// Tüm kiralama durumlarını getirir.
    /// </summary>
    /// <returns>Kiralama durumları listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllRentingConditions()
    {
        var rentingConditions = await _rentingConditionService.GetAllAsync();
        return (rentingConditions == null || !rentingConditions.Any()) ? NotFound("Hiç kiralama durumu bulunamadı.") : Ok(rentingConditions);
    }

    /// <summary>
    /// Belirli bir kiralama durumunu ID'ye göre getirir.
    /// </summary>
    /// <param name="rentingConditionId">Kiralama durumu ID'si.</param>
    /// <returns>Kiralama durumu bilgileri.</returns>
    [HttpGet("GetByRentingConditionId/{rentingConditionId}")]
    public async Task<IActionResult> GetByIdAsync(int rentingConditionId)
    {
        var rentingCondition = await _rentingConditionService.GetByIdAsync(rentingConditionId);
        return rentingCondition == null ? NotFound("Kiralama durumu bulunamadı.") : Ok(rentingCondition);
    }

    /// <summary>
    /// Yeni bir kiralama durumu ekler.
    /// </summary>
    /// <param name="rentingConditionCreateUpdateDTO">Kiralama durumu bilgileri.</param>
    /// <returns>Eklenen kiralama durumu bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(RentingConditionCreateUpdateDTO rentingConditionCreateUpdateDTO)
    {
        var addedRentingCondition = await _rentingConditionService.Add(rentingConditionCreateUpdateDTO);
        return addedRentingCondition == null ? BadRequest("Kiralama durumu eklenirken bir hata oluştu.") : Ok(addedRentingCondition);
    }

    /// <summary>
    /// Mevcut bir kiralama durumunu günceller.
    /// </summary>
    /// <param name="rentingConditionCreateUpdateDTO">Güncellenecek kiralama durumu bilgileri.</param>
    /// <returns>Güncellenen kiralama durumu bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(RentingConditionCreateUpdateDTO rentingConditionCreateUpdateDTO)
    {
        var updatedRentingCondition = await _rentingConditionService.Update(rentingConditionCreateUpdateDTO);
        return updatedRentingCondition == null ? NotFound("Güncellenecek kiralama durumu bulunamadı.") : Ok(updatedRentingCondition);
    }

    /// <summary>
    /// Belirli bir kiralama durumunu ID'ye göre siler.
    /// </summary>
    /// <param name="rentingConditionId">Kiralama durumu ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByRentingConditionId/{rentingConditionId}")]
    public async Task<IActionResult> Delete(int rentingConditionId)
    {
        var isDeleted = await _rentingConditionService.Delete(rentingConditionId);
        return isDeleted ? Ok("Kiralama durumu silindi.") : NotFound("Kiralama durumu bulunamadı.");
    }
}