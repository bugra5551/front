using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Marka bilgilerini temsil eder.
/// </summary>
public class Brand
{
    [Key]
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public ICollection<Model> Models { get; set; }
    public bool isDeleted { get; set; }
}