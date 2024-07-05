using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Çalışanlar ile ilgili işlemleri yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    /// <summary>
    /// EmployeeController sınıfının kurucusu.
    /// </summary>
    /// <param name="employeeService">Çalışan hizmeti.</param>
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// Tüm çalışanları getirir.
    /// </summary>
    /// <returns>Çalışan listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var employees = await _employeeService.GetAllAsync();
        return (employees == null || !employees.Any()) ? NotFound("Hiç çalışan bulunamadı.") : Ok(employees);
    }

    /// <summary>
    /// Belirli bir çalışan ID'sine göre çalışan bilgilerini getirir.
    /// </summary>
    /// <param name="employeeId">Çalışan ID'si.</param>
    /// <returns>Çalışan bilgileri.</returns>
    [HttpGet("GetByEmployeeId/{employeeId}")]
    public async Task<IActionResult> GetByIdAsync(int employeeId)
    {
        var employee = await _employeeService.GetByIdAsync(employeeId);
        return employee == null ? NotFound($"Çalışan bulunamadı.") : Ok(employee);
    }

    /// <summary>
    /// Yeni bir çalışan kaydı ekler.
    /// </summary>
    /// <param name="employeeCreateUpdateDTO">Çalışan bilgileri.</param>
    /// <returns>Kayıt işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(EmployeeCreateUpdateDTO employeeCreateUpdateDTO)
    {
        var addedEmployee = await _employeeService.Add(employeeCreateUpdateDTO);
        return addedEmployee == null ? BadRequest("Çalışan eklenirken bir hata oluştu.") : Ok(addedEmployee);
    }

    /// <summary>
    /// Mevcut bir çalışan kaydını günceller.
    /// </summary>
    /// <param name="employeeCreateUpdateDTO">Güncellenmek istenen çalışan bilgileri.</param>
    /// <returns>Güncellenen çalışan bilgileri.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(EmployeeCreateUpdateDTO employeeCreateUpdateDTO)
    {
        var updatedEmployee = await _employeeService.Update(employeeCreateUpdateDTO);
        return updatedEmployee == null ? NotFound("Güncellenecek çalışan bulunamadı.") : Ok(updatedEmployee);
    }

    /// <summary>
    /// Belirli bir çalışan ID'sine göre çalışan kaydını siler.
    /// </summary>
    /// <param name="employeeId">Silinmek istenen çalışan ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteByEmployeeId/{employeeId}")]
    public async Task<IActionResult> Delete(int employeeId)
    {
        var isDeleted = await _employeeService.Delete(employeeId);
        return isDeleted ? Ok("Çalışan silindi.") : NotFound("Çalışan bulunamadı.");
    }
}
