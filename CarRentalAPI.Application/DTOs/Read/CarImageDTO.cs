namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Araç resimleri veri aktarım nesnesi.
/// </summary>
public class CarImageDTO
{
    public int CarImageId { get; set; }
    public int CarId { get; set; }
    public string ImageData { get; set; } = null!;
    public CarDTO Car { get; set; }
    public bool isDeleted { get; set; }
}
