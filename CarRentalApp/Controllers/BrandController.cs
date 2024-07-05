using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Marka işlemlerini yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    /// <summary>
    /// BrandController sınıfının kurucusu.
    /// </summary>
    /// <param name="brandService">Marka hizmeti.</param>
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    /// <summary>
    /// Tüm markaları getirir.
    /// </summary>
    /// <returns>Marka listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _brandService.GetAllAsync();
        return (brands == null || !brands.Any()) ? NotFound("Hiç marka bulunamadı.") : Ok(brands);
    }

    /// <summary>
    /// Belirli bir marka ID'sine göre marka getirir.
    /// </summary>
    /// <param name="brandId">Marka ID'si.</param>
    /// <returns>Marka bilgileri.</returns>
    [HttpGet("GetByBrandId/{brandId}")]
    public async Task<IActionResult> GetByIdAsync(int brandId)
    {
        var brand = await _brandService.GetByIdAsync(brandId);
        return brand == null ? NotFound("Marka bulunamadı.") : Ok(brand);
    }

    /// <summary>
    /// Yeni bir marka ekler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="brandCreateUpdateDTO">Eklenmek istenen marka bilgileri.</param>
    /// <returns>Eklenen marka bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(BrandCreateUpdateDTO brandCreateUpdateDTO)
    {
        var addedBrand = await _brandService.Add(brandCreateUpdateDTO);
        return addedBrand == null ? BadRequest("Marka eklenirken bir hata oluştu.") : Ok(addedBrand);
    }

    /// <summary>
    /// Mevcut bir markayı günceller. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="brandCreateUpdateDTO">Güncellenmek istenen marka bilgileri.</param>
    /// <returns>Güncellenen marka bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(BrandCreateUpdateDTO brandCreateUpdateDTO)
    {
        var updatedBrand = await _brandService.Update(brandCreateUpdateDTO);
        return updatedBrand == null ? NotFound("Güncellenecek marka bulunamadı.") : Ok(updatedBrand);
    }

    /// <summary>
    /// Belirli bir marka ID'sine göre markayı siler. (Sadece Admin yetkilidir)
    /// </summary>
    /// <param name="brandId">Silinmek istenen marka ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByBrandId/{brandId}")]
    public async Task<IActionResult> Delete(int brandId)
    {
        var isDeleted = await _brandService.Delete(brandId);
        return isDeleted ? Ok("Marka silindi.") : NotFound("Marka bulunamadı.");
    }
}