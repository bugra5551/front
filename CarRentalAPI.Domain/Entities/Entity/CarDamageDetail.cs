using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Araç hasar detaylarını temsil eder.
/// </summary>
public class CarDamageDetail
{
    [Key]
    public int DamageDetailId { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public DateTime DamageDate { get; set; }
    public string Description { get; set; }
    public decimal RepairCost { get; set; }
    public bool isDeleted { get; set; }
}
