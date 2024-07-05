namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Lokasyon veri aktarım nesnesi.
/// </summary>
public class LocationDTO
{
    public int LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public bool isDeleted { get; set; }
    //public ICollection<CarDTO> Cars { get; set; }
}
