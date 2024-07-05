namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Lokasyon oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>

public class LocationCreateUpdateDTO
{
    public int LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public bool isDeleted { get; set; }
}
