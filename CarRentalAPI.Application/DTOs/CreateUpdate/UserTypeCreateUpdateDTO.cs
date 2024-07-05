namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Kullanıcı türü oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class UserTypeCreateUpdateDTO
{
    public int UserTypeId { get; set; }
    public string UserTypeName { get; set; } = null!;
    public bool isDeleted { get; set; }
}
