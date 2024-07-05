namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Araç sınıfı veri aktarım nesnesi.
/// </summary>

public class CarClassDTO
{
    public int CarClassId { get; set; }
    public string CarClassDescription { get; set; } = null!;
    public bool isDeleted { get; set; }

    //public ICollection<CarDTO> Cars { get; set; }
}