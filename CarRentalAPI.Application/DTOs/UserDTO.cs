namespace CarRentalAPI.Application.DTOs;

/// <summary>
/// Kullanıcı veri aktarım nesnesi.
/// </summary>
public class UserDTO
{
    public int UserId { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    //public UserDTO UserType { get; set; } = null!;
    public int UserTypeId { get; set; }
    public string DriverLicenseNumber { get; set; }
}
