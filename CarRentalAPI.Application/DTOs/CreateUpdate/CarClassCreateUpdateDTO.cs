namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Araç sınıfı oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class CarClassCreateUpdateDTO
{
    public int CarClassId { get; set; }
    public string CarClassDescription { get; set; } = null!;
    public bool isDeleted { get; set; }
}
