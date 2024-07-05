namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Ödeme oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>

public class PaymentCreateUpdateDTO
{
    public int PaymentId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string PaymentStatus { get; set; } = null!;
    public bool isDeleted { get; set; }
}