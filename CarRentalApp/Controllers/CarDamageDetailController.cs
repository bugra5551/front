using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araç hasar kayıtlarını yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CarDamageDetailController : ControllerBase
{
    private readonly ICarDamageDetailService _carDamageDetailService;

    /// <summary>
    /// CarDamageDetailController sınıfının kurucusu.
    /// </summary>
    /// <param name="carDamageDetailService">Araç hasar kaydı hizmeti.</param>
    public CarDamageDetailController(ICarDamageDetailService carDamageDetailService)
    {
        _carDamageDetailService = carDamageDetailService;
    }

    /// <summary>
    /// Tüm araç hasar kayıtlarını getirir.
    /// </summary>
    /// <returns>Araç hasar kayıtları listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var carDamageDetails = await _carDamageDetailService.GetAllAsync();
        return (carDamageDetails == null || !carDamageDetails.Any()) ? NotFound("Araçlara ait hasar kaydı bulunamadı.") : Ok(carDamageDetails);
    }

    /// <summary>
    /// Belirli bir hasar kaydı ID'sine göre araç hasar kaydını getirir.
    /// </summary>
    /// <param name="carDamageDetailId">Hasar kaydı ID'si.</param>
    /// <returns>Araç hasar kaydı bilgileri.</returns>
    [HttpGet("GetByDamageDetailId/{carDamageDetailId}")]
    public async Task<IActionResult> GetByIdAsync(int carDamageDetailId)
    {
        var carDamageDetail = await _carDamageDetailService.GetByIdAsync(carDamageDetailId);
        return carDamageDetail == null ? NotFound("Hasar kaydı bulunamadı.") : Ok(carDamageDetail);
    }

    /// <summary>
    /// Belirli bir araç ID'sine göre araç hasar kayıtlarını getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Araca ait hasar kayıtları listesi.</returns>
    [HttpGet("GetByCarId/{carId}")]
    public async Task<IActionResult> GetByCarIdAsync(int carId)
    {
        var carDamageDetailsByCar = await _carDamageDetailService.GetDetailsByCarIdAsync(carId);
        return carDamageDetailsByCar == null ? NotFound("Seçilen araca ait hasar kaydı bulunamadı.") : Ok(carDamageDetailsByCar);
    }

    /// <summary>
    /// Yeni bir araç hasar kaydı ekler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carDamageDetailCreateUpdateDTO">Eklenmek istenen hasar kaydı bilgileri.</param>
    /// <returns>Eklenen hasar kaydı bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CarDamageDetailCreateUpdateDTO carDamageDetailCreateUpdateDTO)
    {
        var addedCarDamageDetail = await _carDamageDetailService.Add(carDamageDetailCreateUpdateDTO);
        return addedCarDamageDetail == null ? BadRequest("Hasar kaydı eklenirken bir hata oluştu.") : Ok(addedCarDamageDetail);
    }

    /// <summary>
    /// Mevcut bir araç hasar kaydını günceller. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carDamageDetailCreateUpdateDTO">Güncellenmek istenen hasar kaydı bilgileri.</param>
    /// <returns>Güncellenen hasar kaydı bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] CarDamageDetailCreateUpdateDTO carDamageDetailCreateUpdateDTO)
    {
        var updatedCarDamageDetail = await _carDamageDetailService.Update(carDamageDetailCreateUpdateDTO);
        return updatedCarDamageDetail == null ? NotFound("Güncellenecek hasar kaydı bulunamadı.") : Ok(updatedCarDamageDetail);
    }

    /// <summary>
    /// Belirli bir hasar kaydı ID'sine göre araç hasar kaydını siler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carDamageDetailId">Silinmek istenen hasar kaydı ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByDamageDetailId/{carDamageDetailId}")]
    public async Task<IActionResult> Delete(int carDamageDetailId)
    {
        var isDeleted = await _carDamageDetailService.Delete(carDamageDetailId);
        return isDeleted ? Ok("Hasar kaydı silindi.") : NotFound("Hasar kaydı bulunamadı.");
    }
}
