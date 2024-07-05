using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araba işlemlerini yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    /// <summary>
    /// CarController sınıfının kurucusu.
    /// </summary>
    /// <param name="carService">Araba hizmeti.</param>
    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    /// <summary>
    /// Tüm arabaları getirir.
    /// </summary>
    /// <returns>Araba listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await _carService.GetAllAsync();
        return (cars == null || !cars.Any()) ? NotFound("Hiç araba bulunamadı.") : Ok(cars);
    }

    /// <summary>
    /// Belirli bir araba ID'sine göre araba getirir.
    /// </summary>
    /// <param name="carId">Araba ID'si.</param>
    /// <returns>Araba bilgileri.</returns>
    [HttpGet("GetByCarId/{carId}")]
    public async Task<IActionResult> GetByIdAsync(int carId)
    {
        var car = await _carService.GetByIdAsync(carId);
        return car == null ? NotFound("Araba bulunamadı.") : Ok(car);
    }

    /// <summary>
    /// Yeni bir araba ekler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carCreateUpdateDTO">Eklenmek istenen araba bilgileri.</param>
    /// <returns>Eklenen araba bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(CarCreateUpdateDTO carCreateUpdateDTO)
    {
        var addedCar = await _carService.Add(carCreateUpdateDTO);
        return addedCar == null ? BadRequest("Araba eklenirken bir hata oluştu.") : Ok(addedCar);
    }

    /// <summary>
    /// Mevcut bir arabayı günceller.
    /// </summary>
    /// <param name="carCreateUpdateDTO">Güncellenmek istenen araba bilgileri.</param>
    /// <returns>Güncellenen araba bilgileri.</returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update(CarCreateUpdateDTO carCreateUpdateDTO)
    {
        var updatedCar = await _carService.Update(carCreateUpdateDTO);
        return updatedCar == null ? NotFound("Güncellenecek araba bulunamadı.") : Ok(updatedCar);
    }

    /// <summary>
    /// Belirli bir araba ID'sine göre arabayı siler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carId">Silinmek istenen araba ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByCarId/{carId}")]
    public async Task<IActionResult> Delete(int carId)
    {
        var isDeleted = await _carService.Delete(carId);
        return isDeleted ? Ok("Araba silindi.") : NotFound("Araba bulunamadı.");
    }
}
