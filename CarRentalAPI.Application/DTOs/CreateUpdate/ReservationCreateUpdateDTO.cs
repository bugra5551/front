namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Rezervasyon oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class ReservationCreateUpdateDTO
{
    public int ReservationId { get; set; }
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime PickUpDate { get; set; }
    public int PickUpLocation { get; set; }
    public DateTime DropOffDate { get; set; }
    public int DropOffLocation { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = null!;
    public int PaymentId { get; set; }
    public bool isDeleted { get; set; }
}
