namespace CarRentalAPI.Application.DTOs.Read;
/// <summary>
/// Yardım masası oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>

public class HelpDeskCreateUpdateDTO
{
    public int UserId { get; set; }
    public int CarId { get; set; }
    public int MessageId { get; set; } // 1: Servis, 0: Müşteri
    public string Message { get; set; }
    public DateTime MessageDate { get; set; }
}