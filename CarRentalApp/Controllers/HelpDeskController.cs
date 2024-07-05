using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// HelpDesk ile ilgili işlemleri yöneten kontrolör.
/// </summary>
[ApiController]
[Route("[controller]")]
public class HelpDeskController : ControllerBase
{
    private readonly IHelpDeskService _helpDeskService;

    /// <summary>
    /// HelpDeskController sınıfının kurucusu.
    /// </summary>
    /// <param name="helpDeskService">HelpDesk hizmeti.</param>
    public HelpDeskController(IHelpDeskService helpDeskService)
    {
        _helpDeskService = helpDeskService;
    }

    /// <summary>
    /// Belirli bir araba ve kullanıcı ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araba ID'si.</param>
    /// <param name="userId">Kullanıcı ID'si.</param>
    /// <returns>Mesajlar listesi.</returns>
    [HttpGet("GetMessages/{carId}/{userId}")]
    public async Task<IActionResult> GetMessagesByCarIdAndUserId(int carId, int userId)
    {
        var messages = await _helpDeskService.GetMessagesByCarIdAndUserId(carId, userId);
        return Ok(messages);
    }

    /// <summary>
    /// Belirli bir araba ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araba ID'si.</param>
    /// <returns>Mesajlar listesi.</returns>
    [HttpGet("GetMessagesByCarId/{carId}")]
    public async Task<IActionResult> GetMessagesByCarId(int carId)
    {
        var messages = await _helpDeskService.GetMessagesByCarId(carId);
        return Ok(messages);
    }

    /// <summary>
    /// Yeni bir mesaj ekler.
    /// </summary>
    /// <param name="messageDTO">Mesaj bilgileri.</param>
    /// <returns>Eklenen mesaj bilgileri.</returns>
    [HttpPost("SendMessage")]
    public async Task<IActionResult> AddMessage([FromBody] HelpDeskCreateUpdateDTO messageDTO)
    {
        var addedMessage = await _helpDeskService.AddMessage(messageDTO);
        return Ok(addedMessage);
    }
}