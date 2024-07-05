using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Yardım masası mesajları için servis arayüzü.
/// </summary>
public interface IHelpDeskService
{
    Task<IEnumerable<HelpDeskDTO>> GetMessagesByCarIdAndUserId(int carId, int userId);
    Task<IEnumerable<HelpDeskDTO>> GetMessagesByCarId(int carId);
    Task<HelpDeskCreateUpdateDTO> AddMessage(HelpDeskCreateUpdateDTO message);
}
