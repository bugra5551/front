namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Araç kiralama koşulları oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class CarRentingConditionCreateUpdateDTO
{
    public int CarRentingConditionId { get; set; }
    public int CarId { get; set; }
    public int RentingConditionId { get; set; }
    public string ConditionValue { get; set; } = null!;
    public bool isDeleted { get; set; }
}
