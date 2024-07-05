using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araba resimleri ile ilgili işlemleri yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CarImageController : ControllerBase
{
    private readonly ICarImageService _carImageService;

    /// <summary>
    /// CarImageController sınıfının kurucusu.
    /// </summary>
    /// <param name="carImageService">Araba resmi hizmeti.</param>
    public CarImageController(ICarImageService carImageService)
    {
           _carImageService = carImageService;
    }

    /// <summary>
    /// Tüm araba resimlerini getirir.
    /// </summary>
    /// <returns>Araba resimleri listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCarImages()
    {
        var carImages = await _carImageService.GetAllAsync();
        return (carImages == null || !carImages.Any()) ? NotFound("Hiç araba resmi bulunamadı.") : Ok(carImages);
    }

    /// <summary>
    /// Belirli bir araba resmi ID'sine göre araba resmini getirir.
    /// </summary>
    /// <param name="carImageId">Araba resmi ID'si.</param>
    /// <returns>Araba resmi bilgileri.</returns>
    [HttpGet("GetImageByCarImageId/{carImageId}")]
    public async Task<IActionResult> GetByIdAsync(int carImageId)
    {
        var carImage = await _carImageService.GetByIdAsync(carImageId);
        return carImage == null ? NotFound("Araba resmi bulunamadı.") : Ok(carImage);
    }

    /// <summary>
    /// Belirli bir araba ID'sine göre araba resimlerini getirir.
    /// </summary>
    /// <param name="carId">Araba ID'si.</param>
    /// <returns>Araba resimleri listesi.</returns>
    [HttpGet("GetImagesByCarId/{carId}")]
    public async Task<IActionResult> GetImagesByCarIdAsync(int carId)
    {
        var carImages = await _carImageService.GetImagesByCarIdAsync(carId);
        return (carImages == null || !carImages.Any()) ? NotFound("Araba resimleri bulunamadı.") : Ok(carImages);
    }

    /// <summary>
    /// Belirli bir araba ID'sine göre ilk araba resmini getirir.
    /// </summary>
    /// <param name="carId">Araba ID'si.</param>
    /// <returns>Arabanın ilk resmi base64 formatında.</returns>
    [HttpGet("GetFirstImageByCarId/{carId}")]
    public async Task<IActionResult> GetCarImages(int carId)
    {
        var imageBase64s = await _carImageService.GetFirstImageBase64(carId);
        return Ok(imageBase64s);
    }

    /// <summary>
    /// Yeni bir araba resmi ekler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carId">Araba ID'si.</param>
    /// <param name="file">Yüklenen dosya.</param>
    /// <returns>Eklenen araba resmi bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromForm] int carId, [FromForm] IFormFile file)
    {
        // Debug için gelen veriler
        Console.WriteLine($"Received carId: {carId}");
        Console.WriteLine($"Received file: {file?.FileName}");

        if (file == null || file.Length == 0)
        {
            return BadRequest("Geçersiz dosya.");
        }

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var base64String = Convert.ToBase64String(memoryStream.ToArray());
            var carImageCreateUpdateDTO = new CarImageCreateUpdateDTO
            {
                CarId = carId,
                ImageData = base64String,
                isDeleted = false
            };

            var addedCarImage = await _carImageService.Add(carImageCreateUpdateDTO);
            return addedCarImage == null ? BadRequest("Araba resmi eklenirken bir hata oluştu.") : Ok(addedCarImage);
        }
    }

    /// <summary>
    /// Mevcut bir araba resmini günceller. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carImageCreateUpdateDTO">Güncellenmek istenen araba resmi bilgileri.</param>
    /// <returns>Güncellenen araba resmi bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(CarImageCreateUpdateDTO carImageCreateUpdateDTO)
    {
        var updatedCarImage = await _carImageService.Update(carImageCreateUpdateDTO);
        return updatedCarImage == null ? NotFound("Güncellenecek araba bulunamadı.") : Ok(updatedCarImage);
    }

    /// <summary>
    /// Belirli bir araba resmi ID'sine göre araba resmini siler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="carImageId">Silinmek istenen araba resmi ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByCarImageId/{carImageId}")]
    public async Task<IActionResult> Delete(int carImageId)
    {
        var isDeleted = await _carImageService.Delete(carImageId);
        return isDeleted ? Ok("Araba resmi silindi.") : NotFound("Araba bulunamadı.");
    }
}
