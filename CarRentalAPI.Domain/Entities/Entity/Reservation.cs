using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Rezervasyon bilgilerini temsil eder.
/// </summary>
public class Reservation
{
    [Key]
    public int ReservationId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime PickUpDate { get; set; }
    public int PickUpLocation { get; set; }
    public DateTime DropOffDate { get; set; }
    public int DropOffLocation { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = null!;
    public int PaymentId { get; set; }
    public Payment Payment { get; set; }
    public bool isDeleted { get; set; }

    [ForeignKey("PickUpLocation")]
    public Location SelectedPickUpLocation { get; set; }

    [ForeignKey("DropOffLocation")]
    public Location SelectedDropOffLocation { get; set; }

}