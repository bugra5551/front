namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Araç modeli veri aktarım nesnesi.
/// </summary>
public class ModelDTO
{
    public int ModelId { get; set; }
    public string ModelName { get; set; }
    public int BrandId { get; set; }
    public BrandDTO Brand { get; set; }
    public bool isDeleted { get; set; }
}
