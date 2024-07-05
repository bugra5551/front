using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Araç bilgilerini temsil eder.
/// </summary>
public class Car
{
    [Key]
    public int CarId { get; set; }
    public string LicensePlate { get; set; } = null!;
    public int CarClassId { get; set; }
    public CarClass CarClass { get; set; }
    public bool Gearshift { get; set; }
    public int Passengers { get; set; }
    public int Bags { get; set; }
    public int ModelId { get; set; }
    public Model Model { get; set; }
    public int Doors { get; set; }
    public int Year { get; set; }
    public string Color { get; set; } = null!;
    public decimal DailyPrice { get; set; }
    public string Status { get; set; } = null!;
    public string FuelType { get; set; } = null!;
    public string CarType { get; set; } = null!;
    public int Km { get; set; }
    public string ChassisNo { get; set; } = null!;
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<CarSpecification> CarSpecifications { get; set; }
    public ICollection<CarRentingCondition> CarRentingConditions { get; set; }
    public ICollection<CarDamageDetail> CarDamageDetails { get; set; }
    public bool isDeleted { get; set; }

    //public ICollection<CarImage> CarImages { get; set; }
}
