using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Araç resimlerini temsil eder.
/// </summary>
public class CarImage
{
    [Key]
    public int CarImageId { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public string ImageData { get; set; } = null!;
    public bool isDeleted { get; set; }
}
