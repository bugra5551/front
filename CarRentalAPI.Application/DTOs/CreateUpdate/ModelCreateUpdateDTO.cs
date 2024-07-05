namespace CarRentalAPI.Application.DTOs.CreateUpdate;
/// <summary>
/// Araç modeli oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>

public class ModelCreateUpdateDTO
{
    public int ModelId { get; set; }
    public string ModelName { get; set; }
    public int BrandId { get; set; }
    public bool isDeleted { get; set; }
}
