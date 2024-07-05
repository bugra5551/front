namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Araç kiralama koşulları veri aktarım nesnesi.
/// </summary>
public class CarRentingConditionDTO
{
    public int CarRentingConditionId { get; set; }
    public int CarId { get; set; }
    public CarDTO Car { get; set; }
    public int RentingConditionId { get; set; }
    public RentingConditionDTO RentingCondition { get; set; }
    public string ConditionValue { get; set; } = null!;
    public bool isDeleted { get; set; }
}
