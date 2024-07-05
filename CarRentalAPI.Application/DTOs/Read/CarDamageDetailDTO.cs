namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Araç hasar detayı veri aktarım nesnesi.
/// </summary>

public class CarDamageDetailDTO
{
    public int DamageDetailId { get; set; }
    public int CarId { get; set; }
    public CarDTO Car { get; set; }
    public DateTime DamageDate { get; set; }
    public string Description { get; set; }
    public decimal RepairCost { get; set; }
    public bool isDeleted { get; set; }
}
