using CarRentalAPI.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Kimlik doğrulama işlemlerini yöneten kontrolör.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    /// <summary>
    /// AuthController sınıfının kurucusu.
    /// </summary>
    /// <param name="configuration">Yapılandırma ayarlarını içerir.</param>
    /// <param name="userService">Kullanıcı hizmetini sağlar.</param>
    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    /// <summary>
    /// Kullanıcı giriş işlemini gerçekleştirir.
    /// </summary>
    /// <param name="login">Giriş bilgilerini içeren model.</param>
    /// <returns>Başarılı girişlerde JWT token döner.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        var user = _userService.Authenticate(login.Username, login.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);
        return Ok(new { token });
    }

    /// <summary>
    /// Kullanıcı için JWT token oluşturur.
    /// </summary>
    /// <param name="user">Token oluşturulacak kullanıcı.</param>
    /// <returns>Oluşturulan JWT token string olarak döner.</returns>
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.UserType.UserTypeName),
            new Claim(JwtRegisteredClaimNames.Name, (user.FirstName + " " + user.LastName)),
            new Claim(ClaimTypes.NameIdentifier , user.UserId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
