using CarRentalAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Yardım masası mesajlarını temsil eder.
/// </summary>
public class HelpDesk
{
    [Key]
    public int ChatId { get; set; }
    public int CarId { get; set; }
    public int UserId { get; set; }
    public User User {  get; set; } 
    public int MessageId { get; set; } // 1: Admin ya da Employee, 0: Müşteri
    public string Message { get; set; }
    public DateTime MessageDate { get; set; }
}