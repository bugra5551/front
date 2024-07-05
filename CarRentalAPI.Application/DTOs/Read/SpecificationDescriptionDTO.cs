namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Özellik açıklaması veri aktarım nesnesi.
/// </summary>

public class SpecificationDescriptionDTO
{
    public int SpecificationDescriptionId { get; set; }
    public string Description { get; set; } = null!;
    public bool isDeleted { get; set; }

    //public ICollection<CarSpecificationDTO> CarSpecifications { get; set; }
}
