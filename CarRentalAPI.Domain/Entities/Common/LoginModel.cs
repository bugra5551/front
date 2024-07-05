namespace CarRentalAPI.Domain.Entities.Common;

/// <summary>
/// Giriş bilgilerini temsil eder.
/// </summary>
public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
