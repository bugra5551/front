using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Kiralama koşullarını temsil eder.
/// </summary>
public class RentingCondition
{
    [Key]
    public int RentingConditionId { get; set; }
    public string RentingConditionName { get; set; } = null!;
    public string RentingConditionDescription { get; set; } = null!;
    public bool isDeleted { get; set; }

    //public ICollection<CarRentingCondition> CarRentingConditions { get; set; }
}
