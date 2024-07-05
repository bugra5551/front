namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Araç özellikleri veri aktarım nesnesi.
/// </summary>
public class CarSpecificationDTO
{
    public int CarSpecificationId { get; set; }
    public int CarId { get; set; }
    public CarDTO Car { get; set; }
    public int SpecificationDescriptionId { get; set; }
    public SpecificationDescriptionDTO SpecificationDescription { get; set; }
    public bool isDeleted { get; set; }
}
