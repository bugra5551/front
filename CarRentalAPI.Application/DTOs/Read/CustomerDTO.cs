namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Müşteri veri aktarım nesnesi.
/// </summary>
public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string DriverLicenseNumber { get; set; } = null!;
    public ICollection<ReservationDTO> Reservations { get; set; }
    public bool isDeleted { get; set; }
}
