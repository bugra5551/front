using CarRentalAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Müşteri bilgilerini temsil eder.
/// </summary>
public class Customer
{
    [Key]
    public int UserId { get; set; }
    public int CustomerId { get; set; }
    public string DriverLicenseNumber { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; }
    public bool isDeleted { get; set; }
}