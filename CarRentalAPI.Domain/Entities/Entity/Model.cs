using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Araç modeli bilgilerini temsil eder.
/// </summary>
public class Model
{
    [Key]
    public int ModelId { get; set; }
    public string ModelName { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public ICollection<Car> Cars { get; set; }
    public bool isDeleted { get; set; }
}