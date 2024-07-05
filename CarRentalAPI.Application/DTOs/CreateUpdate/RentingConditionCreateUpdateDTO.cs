namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Kiralama koşulları oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>

public class RentingConditionCreateUpdateDTO
{
    public int RentingConditionId { get; set; }
    public string RentingConditionName { get; set; } = null!;
    public string RentingConditionDescription { get; set; } = null!;
    public bool isDeleted { get; set; }
}