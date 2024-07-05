namespace CarRentalAPI.Application.DTOs.Read;

/// <summary>
/// Çalışan veri aktarım nesnesi.
/// </summary>
public class EmployeeDTO : UserDTO
{
    public int EmployeeId { get; set; }
    public string JobTitle { get; set; } = null!;
    public bool isDeleted { get; set; }
}