using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Araç özelliklerini temsil eder.
/// </summary>
public class CarSpecification
{

    [Key]
    public int CarSpecificationId { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public int SpecificationDescriptionId { get; set; }
    public SpecificationDescription SpecificationDescription { get; set; }
    public bool isDeleted { get; set; }
}
