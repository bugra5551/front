using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Kullanıcı tipleri ile ilgili işlemleri yöneten kontrolör.
/// </summary>
public class UserTypeController : ControllerBase
{
    private readonly IUserTypeService _userTypeService;

    /// <summary>
    /// UserTypeController sınıfının kurucusu.
    /// </summary>
    /// <param name="userTypeService">Kullanıcı tipi hizmeti.</param>
    public UserTypeController(IUserTypeService userTypeService)
    {
        _userTypeService = userTypeService;
    }

    /// <summary>
    /// Tüm kullanıcı tiplerini getirir.
    /// </summary>
    /// <returns>Kullanıcı tipleri listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllModels()
    {
        var models = await _userTypeService.GetAllAsync();
        return (models == null || !models.Any()) ? NotFound("Kullanıcı tipi bulunamadı.") : Ok(models);
    }

    /// <summary>
    /// Belirli bir kullanıcı tipini ID'ye göre getirir.
    /// </summary>
    /// <param name="userTypeId">Kullanıcı tipi ID'si.</param>
    /// <returns>Kullanıcı tipi bilgileri.</returns>
    [HttpGet("GetByUserTypeId/{userTypeId}")]
    public async Task<IActionResult> GetByIdAsync(int userTypeId)
    {
        var userType = await _userTypeService.GetByIdAsync(userTypeId);
        return userType == null ? NotFound("Kullanıcı tipi bulunamadı.") : Ok(userType);
    }

    /// <summary>
    /// Yeni bir kullanıcı tipi ekler.
    /// </summary>
    /// <param name="userTypeCreateUpdateDTO">Kullanıcı tipi bilgileri.</param>
    /// <returns>Eklenen kullanıcı tipi bilgileri.</returns>
    [HttpPost("Add")]
    public async Task<IActionResult> Add(UserTypeCreateUpdateDTO userTypeCreateUpdateDTO)
    {
        var addedModel = await _userTypeService.Add(userTypeCreateUpdateDTO);
        return addedModel == null ? BadRequest("Araç modeli eklenirken bir hata oluştu.") : Ok(addedModel);
    }

    /// <summary>
    /// Mevcut bir kullanıcı tipini günceller.
    /// </summary>
    /// <param name="userTypeCreateUpdateDTO">Güncellenecek kullanıcı tipi bilgileri.</param>
    /// <returns>Güncellenen kullanıcı tipi bilgileri.</returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update(UserTypeCreateUpdateDTO userTypeCreateUpdateDTO)
    {
        var updatedModel = await _userTypeService.Update(userTypeCreateUpdateDTO);
        return updatedModel == null ? NotFound("Güncellenecek araç modeli bulunamadı.") : Ok(updatedModel);
    }

    /// <summary>
    /// Belirli bir kullanıcı tipini ID'ye göre siler.
    /// </summary>
    /// <param name="userTypeId">Kullanıcı tipi ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [HttpDelete("DeleteByUserTypeId/{userTypeId}")]
    public async Task<IActionResult> Delete(int userTypeId)
    {
        var isDeleted = await _userTypeService.Delete(userTypeId);
        return isDeleted ? Ok("Kullanıcı tipi silindi.") : NotFound("Kullanıcı Tipi bulunamadı.");
    }
}
