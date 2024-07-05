namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Araç resimleri oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class CarImageCreateUpdateDTO
{
    public int CarImageId { get; set; }
    public int CarId { get; set; }
    public string ImageData { get; set; } = null!;
    public bool isDeleted { get; set; }
}
