namespace CarRentalAPI.Application.DTOs.CreateUpdate;

/// <summary>
/// Müşteri oluşturma ve güncelleme bilgilerini temsil eder.
/// </summary>

public class CustomerCreateUpdateDTO
{
    public int UserId { get; set; }
    public int CustomerId { get; set; }
    public string DriverLicenseNumber { get; set; }
    public bool isDeleted { get; set; }
}
