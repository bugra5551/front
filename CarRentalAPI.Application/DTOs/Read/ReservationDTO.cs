namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Rezervasyon veri aktarım nesnesi.
/// </summary>

public class ReservationDTO
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
    public CustomerDTO Customer { get; set; }
    public CarDTO Car { get; set; }
    public PaymentDTO Payment { get; set; }
    public int PaymentId { get; set; }
    public bool isDeleted { get; set; }
    public LocationDTO SelectedPickUpLocation { get; set; }
    public LocationDTO SelectedDropOffLocation { get; set; }
}
