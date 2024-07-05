using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araç sınıfı işlemlerini yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CarClassController : ControllerBase
{
    private readonly ICarClassService _carClassService;

    /// <summary>
    /// CarClassController sınıfının kurucusu.
    /// </summary>
    /// <param name="carClassService">Araç sınıfı hizmeti.</param>
    public CarClassController(ICarClassService carClassService)
    {
        _carClassService = carClassService;
    }

    /// <summary>
    /// Tüm araç sınıflarını getirir.
    /// </summary>
    /// <returns>Araç sınıfı listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCarClasses()
    {
        var carClasses = await _carClassService.GetAllAsync();
        return (carClasses == null || !carClasses.Any()) ? NotFound("Hiç araç sınıfı bulunamadı.") : Ok(carClasses);
    }

    /// <summary>
    /// Belirli bir araç sınıfı ID'sine göre araç sınıfını getirir.
    /// </summary>
    /// <param name="carClassId">Araç sınıfı ID'si.</param>
    /// <returns>Araç sınıfı bilgileri.</returns>
    [HttpGet("GetByCarClassId/{carClassId}")]
    public async Task<IActionResult> GetByIdAsync(int carClassId)
    {
        var carClass = await _carClassService.GetByIdAsync(carClassId);
        return carClass == null ? NotFound("Araç sınıfı bulunamadı.") : Ok(carClass);
    }

    /// <summary>
    /// Yeni bir araç sınıfı ekler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carClassCreateUpdateDTO">Eklenmek istenen araç sınıfı bilgileri.</param>
    /// <returns>Eklenen araç sınıfı bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(CarClassCreateUpdateDTO carClassCreateUpdateDTO)
    {
        var addedCarClass = await _carClassService.Add(carClassCreateUpdateDTO);
        return addedCarClass == null ? BadRequest("Araç sınıfı eklenirken bir hata oluştu.") : Ok(addedCarClass);
    }

    /// <summary>
    /// Mevcut bir araç sınıfını günceller. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carClassCreateUpdateDTO">Güncellenmek istenen araç sınıfı bilgileri.</param>
    /// <returns>Güncellenen araç sınıfı bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(CarClassCreateUpdateDTO carClassCreateUpdateDTO)
    {
        var updatedCarClass = await _carClassService.Update(carClassCreateUpdateDTO);
        return updatedCarClass == null ? NotFound("Güncellenecek araç sınıfı bulunamadı.") : Ok(updatedCarClass);
    }

    /// <summary>
    /// Belirli bir araç sınıfı ID'sine göre araç sınıfını siler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carClassId">Silinmek istenen araç sınıfı ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByCarClassId/{carClassId}")]
    public async Task<IActionResult> Delete(int carClassId)
    {
        var isDeleted = await _carClassService.Delete(carClassId);
        return isDeleted ? Ok("Araç sınıfı silindi.") : NotFound("Araç sınıfı bulunamadı.");
    }
}
