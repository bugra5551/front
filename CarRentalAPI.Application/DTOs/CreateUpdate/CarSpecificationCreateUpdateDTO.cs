namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Araç özellikleri oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>

public class CarSpecificationCreateUpdateDTO
{
    public int CarSpecificationId { get; set; }
    public int CarId { get; set; }
    public int SpecificationDescriptionId { get; set; }
    public bool isDeleted { get; set; }
}