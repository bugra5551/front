using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araba özellikleri ile ilgili işlemleri yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CarSpecificationController : ControllerBase
{
    private readonly ICarSpecificationService _carSpecificationService;

    /// <summary>
    /// CarSpecificationController sınıfının kurucusu.
    /// </summary>
    /// <param name="carSpecificationService">Araba özelliği hizmeti.</param>
    public CarSpecificationController(ICarSpecificationService carSpecificationService)
    {
        _carSpecificationService = carSpecificationService;
    }

    /// <summary>
    /// Tüm araba özelliklerini getirir.
    /// </summary>
    /// <returns>Araba özellikleri listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCarSpecifications()
    {
        var carSpecifications = await _carSpecificationService.GetAllAsync();
        return (carSpecifications == null || !carSpecifications.Any()) ? NotFound("Hiç araba özelliği bulunamadı.") : Ok(carSpecifications);
    }

    /// <summary>
    /// Belirli bir araba özelliği ID'sine göre araba özelliğini getirir.
    /// </summary>
    /// <param name="carSpecificationId">Araba özelliği ID'si.</param>
    /// <returns>Araba özelliği bilgileri.</returns>
    [HttpGet("GetByCarSpecificationId/{carSpecificationId}")]
    public async Task<IActionResult> GetByIdAsync(int carSpecificationId)
    {
        var carSpecification = await _carSpecificationService.GetByIdAsync(carSpecificationId);
        return carSpecification == null ? NotFound("Araba özelliği bulunamadı.") : Ok(carSpecification);
    }

    /// <summary>
    /// Yeni bir araba özelliği ekler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carSpecificationCreateUpdateDTO">Eklenecek araba özelliği bilgileri.</param>
    /// <returns>Eklenen araba özelliği bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(CarSpecificationCreateUpdateDTO carSpecificationCreateUpdateDTO)
    {
        var addedCarSpecification = await _carSpecificationService.Add(carSpecificationCreateUpdateDTO);
        return addedCarSpecification == null ? BadRequest("Araba özelliği eklenirken bir hata oluştu.") : Ok(addedCarSpecification);
    }

    /// <summary>
    /// Mevcut bir araba özelliğini günceller. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carSpecificationCreateUpdateDTO">Güncellenmek istenen araba özelliği bilgileri.</param>
    /// <returns>Güncellenen araba özelliği bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(CarSpecificationCreateUpdateDTO carSpecificationCreateUpdateDTO)
    {
        var updatedCarSpecification = await _carSpecificationService.Update(carSpecificationCreateUpdateDTO);
        return updatedCarSpecification == null ? NotFound("Güncellenecek araba özelliği bulunamadı.") : Ok(updatedCarSpecification);
    }

    /// <summary>
    /// Belirli bir araba özelliği ID'sine göre araba özelliğini siler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carSpecificationId">Silinmek istenen araba özelliği ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByCarSpecificationId/{carSpecificationId}")]
    public async Task<IActionResult> Delete(int carSpecificationId)
    {
        var isDeleted = await _carSpecificationService.Delete(carSpecificationId);
        return isDeleted ? Ok("Araba özelliği silindi.") : NotFound("Araba özelliği bulunamadı.");
    }
}