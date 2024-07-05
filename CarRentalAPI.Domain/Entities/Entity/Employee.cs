using CarRentalAPI.Domain.Entities.Common;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Çalışan bilgilerini temsil eder.
/// </summary>
public class Employee : User
{
    public int EmployeeId { get; set; }
    public string JobTitle { get; set; } = null!;
    public bool isDeleted { get; set; }
}
