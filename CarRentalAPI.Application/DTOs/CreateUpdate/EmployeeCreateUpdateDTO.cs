namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Çalışan oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>
public class EmployeeCreateUpdateDTO
{
    public int UserId { get; set; }
    public int EmployeeId { get; set; }
    public string JobTitle { get; set; } = null!;
    public bool isDeleted { get; set; }
}
