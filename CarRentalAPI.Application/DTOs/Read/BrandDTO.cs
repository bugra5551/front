namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Marka veri aktarım nesnesi.
/// </summary>
public class BrandDTO
{
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public ICollection<ModelDTO> Models { get; set; }
    public bool isDeleted { get; set; }
}
