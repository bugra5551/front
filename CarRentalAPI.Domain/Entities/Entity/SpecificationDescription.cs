using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Özellik açıklamalarını temsil eder.
/// </summary>
public class SpecificationDescription
{
    [Key]
    public int SpecificationDescriptionId { get; set; }
    public string Description { get; set; } = null!;
    public bool isDeleted { get; set; }

    // public ICollection<CarSpecification> CarSpecifications { get; set; }
}