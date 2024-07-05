using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Konum işlemlerini yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    /// <summary>
    /// LocationController sınıfının kurucusu.
    /// </summary>
    /// <param name="locationService">Konum hizmeti.</param>
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Tüm konum bilgilerini getirir.
    /// </summary>
    /// <returns>Konum bilgileri listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var locations = await _locationService.GetAllAsync();
        return (locations == null || !locations.Any()) ? NotFound("Hiç konum bulunamadı.") : Ok(locations);
    }

    /// <summary>
    /// Belirli bir konum bilgisi ID'ye göre getirir.
    /// </summary>
    /// <param name="locationId">Konum ID'si.</param>
    /// <returns>Konum bilgisi.</returns>
    [HttpGet("GetByLocationId/{locationId}")]
    public async Task<IActionResult> GetByIdAsync(int locationId)
    {
        var location = await _locationService.GetByIdAsync(locationId);
        return location == null ? NotFound("Konum bulunamadı.") : Ok(location);
    }

    /// <summary>
    /// Yeni bir konum ekler.
    /// </summary>
    /// <param name="locationCreateUpdateDTO">Konum bilgileri.</param>
    /// <returns>Eklenen konum bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(LocationCreateUpdateDTO locationCreateUpdateDTO)
    {
        if (locationCreateUpdateDTO == null)
        {
            return BadRequest("Gönderilen veri null.");
        }

        Console.WriteLine("LocationName.: " + locationCreateUpdateDTO.LocationName);
        Console.WriteLine("Address.: " + locationCreateUpdateDTO.Address);
        Console.WriteLine("City.: " + locationCreateUpdateDTO.City);
        Console.WriteLine("Country.: " + locationCreateUpdateDTO.Country);


        var addedLocation = await _locationService.Add(locationCreateUpdateDTO);
        return addedLocation == null ? BadRequest("Konum eklenirken bir hata oluştu.") : Ok(addedLocation);
    }

    /// <summary>
    /// Mevcut bir konumu günceller.
    /// </summary>
    /// <param name="locationCreateUpdateDTO">Güncellenecek konum bilgileri.</param>
    /// <returns>Güncellenen konum bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(LocationCreateUpdateDTO locationCreateUpdateDTO)
    {
        var updatedLocation = await _locationService.Update(locationCreateUpdateDTO);
        return updatedLocation == null ? NotFound("Güncellenecek konum bulunamadı.") : Ok(updatedLocation);
    }

    /// <summary>
    /// Belirli bir konumu ID'ye göre siler.
    /// </summary>
    /// <param name="locationId">Konum ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("DeleteByLocationId/{locationId}")]
    public async Task<IActionResult> Delete(int locationId)
    {
        var isDeleted = await _locationService.Delete(locationId);
        return isDeleted ? Ok("Konum silindi.") : BadRequest("Konuma ait araç olduğundan silinemedi.");
    }
}
