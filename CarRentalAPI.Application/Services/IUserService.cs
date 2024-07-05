using CarRentalAPI.Application.DTOs;
using CarRentalAPI.Domain.Entities.Common;

/// <summary>
/// Kullanıcı kimlik doğrulama ve kullanıcı işlemleri için servis arayüzü.
/// </summary>
public interface IUserService
{
    User Authenticate(string username, string password);
    Task<UserDTO> Add(UserDTO user);
}