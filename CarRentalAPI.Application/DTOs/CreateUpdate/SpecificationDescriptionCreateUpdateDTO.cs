namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Özellik açıklaması oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class SpecificationDescriptionCreateUpdateDTO
{
    public int SpecificationDescriptionId { get; set; }
    public string Description { get; set; } = null!;
    public bool isDeleted { get; set; }
}
