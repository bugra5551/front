using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Araç kiralama koşullarını temsil eder.
/// </summary>
public class CarRentingCondition
{
    [Key]
    public int CarRentingConditionId { get; set; }

    public int CarId { get; set; }
    public Car Car { get; set; }
    public int RentingConditionId { get; set; }
    public RentingCondition RentingCondition { get; set; }
    public string ConditionValue { get; set; } = null!;
    public bool isDeleted { get; set; }

}