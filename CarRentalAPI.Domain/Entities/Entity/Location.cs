using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Lokasyon bilgilerini temsil eder.
/// </summary>
public class Location
{
    [Key]
    public int LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public bool isDeleted { get; set; }

    //public ICollection<Car> Cars { get; set; }
}
