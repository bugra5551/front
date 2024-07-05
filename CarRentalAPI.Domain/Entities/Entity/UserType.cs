using CarRentalAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.Entities.Entity;

/// <summary>
/// Kullanıcı türlerini temsil eder.
/// </summary>
public class UserType
{
    [Key]
    public int UserTypeId { get; set; }
    public string UserTypeName { get; set; } = null!;
    public ICollection<User> Users { get; set; }
}
