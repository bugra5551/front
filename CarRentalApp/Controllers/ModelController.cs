using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Araç modelleri ile ilgili işlemleri yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class ModelController : ControllerBase
{
    private readonly IModelService _modelService;

    /// <summary>
    /// ModelController sınıfının kurucusu.
    /// </summary>
    /// <param name="modelService">Araç modelleri hizmeti.</param>
    public ModelController(IModelService modelService)
    {
        _modelService = modelService;
    }

    /// <summary>
    /// Tüm araç modellerini getirir.
    /// </summary>
    /// <returns>Araç modelleri listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllModels()
    {
        var models = await _modelService.GetAllAsync();
        return (models == null || !models.Any()) ? NotFound("Araç bulunamadı.") : Ok(models);
    }

    /// <summary>
    /// Belirli bir araç modelini ID'ye göre getirir.
    /// </summary>
    /// <param name="modelId">Araç modeli ID'si.</param>
    /// <returns>Araç modeli bilgileri.</returns>
    [HttpGet("GetByModelId/{modelId}")]
    public async Task<IActionResult> GetByIdAsync(int modelId)
    {
        var model = await _modelService.GetByIdAsync(modelId);
        return model == null ? NotFound("Araç modeli bulunamadı.") : Ok(model);
    }

    /// <summary>
    /// Belirli bir markaya ait araç modellerini getirir.
    /// </summary>
    /// <param name="brandId">Marka ID'si.</param>
    /// <returns>Markaya ait araç modelleri listesi.</returns>
    [HttpGet("GetByModelsByBrandId/{brandId}")]
    public async Task<IActionResult> GetByBrandIdAsync(int brandId)
    {
        var models = await _modelService.GetModelsByBrandIdAsync(brandId);
        return models == null ? NotFound("Araç modeli bulunamadı.") : Ok(models);
    }

    /// <summary>
    /// Yeni bir araç modeli ekler.
    /// </summary>
    /// <param name="modelCreateUpdateDTO">Araç modeli bilgileri.</param>
    /// <returns>Eklenen araç modeli bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(ModelCreateUpdateDTO modelCreateUpdateDTO)
    {
        var addedModel = await _modelService.Add(modelCreateUpdateDTO);
        return addedModel == null ? BadRequest("Araç modeli eklenirken bir hata oluştu.") : Ok(addedModel);
    }

    /// <summary>
    /// Mevcut bir araç modelini günceller.
    /// </summary>
    /// <param name="modelCreateUpdateDTO">Güncellenecek araç modeli bilgileri.</param>
    /// <returns>Güncellenen araç modeli bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(ModelCreateUpdateDTO modelCreateUpdateDTO)
    {
        var updatedModel = await _modelService.Update(modelCreateUpdateDTO);
        return updatedModel == null ? NotFound("Güncellenecek araç modeli bulunamadı.") : Ok(updatedModel);
    }

    /// <summary>
    /// Belirli bir araç modelini ID'ye göre siler.
    /// </summary>
    /// <param name="modelId">Araç modeli ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByModelId/{modelId}")]
    public async Task<IActionResult> Delete(int modelId)
    {
        var isDeleted = await _modelService.Delete(modelId);
        return isDeleted ? Ok("Araç modeli silindi.") : NotFound("Araç modeli bulunamadı.");
    }

}