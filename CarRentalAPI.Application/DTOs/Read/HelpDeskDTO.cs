namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Yardım masası veri aktarım nesnesi.
/// </summary>
public class HelpDeskDTO
{
    public int ChatId { get; set; }
    public int CarId { get; set; }
    public int UserId { get; set; }
    public UserDTO User { get; set; }
    public int MessageId { get; set; } // 1: Servis, 0: Müşteri
    public string Message { get; set; }
    public DateTime MessageDate { get; set; }
}