namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Kullanıcı türü veri aktarım nesnesi.
/// </summary>

public class UserTypeDTO
{
    public int UserTypeId { get; set; }
    public string UserTypeName { get; set; } = null!;
    public ICollection<UserDTO> User { get; set; }
}
