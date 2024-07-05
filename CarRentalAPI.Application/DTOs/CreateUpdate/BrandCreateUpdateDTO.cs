namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Marka oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class BrandCreateUpdateDTO
{
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public bool isDeleted { get; set; }
}