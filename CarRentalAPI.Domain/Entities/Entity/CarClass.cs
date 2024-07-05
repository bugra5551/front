using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Araç sınıfı bilgilerini temsil eder.
/// </summary>
public class CarClass
{
    [Key]
    public int CarClassId { get; set; }
    public string CarClassDescription { get; set; } = null!;
    public bool isDeleted { get; set; }

    //public ICollection<Car> Cars { get; set; }
}
