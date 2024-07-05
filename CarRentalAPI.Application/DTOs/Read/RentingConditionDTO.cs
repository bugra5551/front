namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Kiralama koşulları veri aktarım nesnesi.
/// </summary>

public class RentingConditionDTO
{
    public int RentingConditionId { get; set; }
    public string RentingConditionName { get; set; } = null!;
    public string RentingConditionDescription { get; set; } = null!;
   // public ICollection<CarRentingConditionDTO> CarRentingConditions { get; set; }
    public bool isDeleted { get; set; }
}
