namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Araç veri aktarım nesnesi.
/// </summary>

public class CarDTO
{
    public int CarId { get; set; }
    public string LicensePlate { get; set; } = null!;
    public int CarClassId { get; set; }
    public bool Gearshift { get; set; }
    public int Passengers { get; set; }
    public int Bags { get; set; }
    public int ModelId { get; set; }
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
    public CarClassDTO CarClass { get; set; }
    public LocationDTO Location { get; set; }
    public ModelDTO Model { get; set; }
    public ICollection<ReservationDTO> Reservations { get; set; }
    public ICollection<CarSpecificationDTO> CarSpecifications { get; set; }
    public ICollection<CarRentingConditionDTO> CarRentingConditions { get; set; }
    //public ICollection<CarImageDTO> CarImages { get; set; }
    public ICollection<CarDamageDetailDTO> CarDamageDetails { get; set; }
    public bool isDeleted { get; set; }
}