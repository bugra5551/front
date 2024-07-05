namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Ödeme veri aktarım nesnesi.
/// </summary>
public class PaymentDTO
{
    public int PaymentId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string PaymentStatus { get; set; } = null!;
    public bool isDeleted { get; set; }
}
