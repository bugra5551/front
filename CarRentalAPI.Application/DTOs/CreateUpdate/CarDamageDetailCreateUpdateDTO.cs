namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Araç hasar detayları oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class CarDamageDetailCreateUpdateDTO
{
    public int DamageDetailId { get; set; }
    public int CarId { get; set; }
    public DateTime DamageDate { get; set; }
    public string Description { get; set; }
    public decimal RepairCost { get; set; }
    public bool isDeleted { get; set; }
}
