using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araç özellik açıklamaları ile ilgili işlemleri yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class SpecificationDescriptionController : ControllerBase
{
    private readonly ISpecificationDescriptionService _specificationDescriptionService;

    /// <summary>
    /// SpecificationDescriptionController sınıfının kurucusu.
    /// </summary>
    /// <param name="specificationDescriptionService">Araç özellik açıklamaları hizmeti.</param>
    public SpecificationDescriptionController(ISpecificationDescriptionService specificationDescriptionService)
    {
        _specificationDescriptionService = specificationDescriptionService;
    }

    /// <summary>
    /// Tüm araç özellik açıklamalarını getirir.
    /// </summary>
    /// <returns>Araç özellik açıklamaları listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllSpecificationDescriptions()
    {
        var specificationDescriptions = await _specificationDescriptionService.GetAllAsync();
        return (specificationDescriptions == null || !specificationDescriptions.Any()) ? NotFound("Araç özellikler açıklaması bulunamadı.") : Ok(specificationDescriptions);
    }

    /// <summary>
    /// Belirli bir araç özellik açıklamasını ID'ye göre getirir.
    /// </summary>
    /// <param name="specificationDescriptionId">Araç özellik açıklaması ID'si.</param>
    /// <returns>Araç özellik açıklaması bilgileri.</returns>
    [HttpGet("GetBySpecificationDescriptionId/{specificationDescriptionId}")]
    public async Task<IActionResult> GetByIdAsync(int specificationDescriptionId)
    {
        var specificationDescription = await _specificationDescriptionService.GetByIdAsync(specificationDescriptionId);
        return specificationDescription == null ? NotFound("Araç özellik açıklaması bulunamadı.") : Ok(specificationDescription);
    }

    /// <summary>
    /// Yeni bir araç özellik açıklaması ekler.
    /// </summary>
    /// <param name="specificationDescriptionCreateUpdateDTO">Araç özellik açıklaması bilgileri.</param>
    /// <returns>Eklenen araç özellik açıklaması bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(SpecificationDescriptionCreateUpdateDTO specificationDescriptionCreateUpdateDTO)
    {
        var addedSpecificationDescription = await _specificationDescriptionService.Add(specificationDescriptionCreateUpdateDTO);
        return addedSpecificationDescription == null ? BadRequest("Araç özellik açıklaması eklenirken bir hata oluştu.") : Ok(addedSpecificationDescription);
    }

    /// <summary>
    /// Mevcut bir araç özellik açıklamasını günceller.
    /// </summary>
    /// <param name="specificationDescriptionCreateUpdateDTO">Güncellenecek araç özellik açıklaması bilgileri.</param>
    /// <returns>Güncellenen araç özellik açıklaması bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(SpecificationDescriptionCreateUpdateDTO specificationDescriptionCreateUpdateDTO)
    {
        var updatedSpecificationDescription = await _specificationDescriptionService.Update(specificationDescriptionCreateUpdateDTO);
        return updatedSpecificationDescription == null ? NotFound("Güncellenecek araç özellik açıklaması bulunamadı.") : Ok(updatedSpecificationDescription);
    }

    /// <summary>
    /// Belirli bir araç özellik açıklamasını ID'ye göre siler.
    /// </summary>
    /// <param name="specificationDescriptionId">Araç özellik açıklaması ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteBySpecificationDescriptionId/{specificationDescriptionId}")]
    public async Task<IActionResult> Delete(int specificationDescriptionId)
    {
        var isDeleted = await _specificationDescriptionService.Delete(specificationDescriptionId);
        return isDeleted ? Ok("Araç özellik açıklaması silindi.") : NotFound("Araç özellik açıklaması bulunamadı.");
    }
}