using CarRentalAPI.Domain.Entities.Common;

namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Kullanıcı CRUD operasyonları ve doğrulama işlemleri için depo arayüzü.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// E-posta ve şifre ile kullanıcıyı doğrular.
    /// </summary>
    /// <param name="email">Kullanıcının e-posta adresi.</param>
    /// <param name="password">Kullanıcının şifresi.</param>
    /// <returns>Doğrulanan kullanıcı.</returns>
    User GetUserByEmailAndPassword(string email, string password);

    /// <summary>
    /// Yeni bir kullanıcı ekler.
    /// </summary>
    /// <param name="user">Eklenecek kullanıcı bilgileri.</param>
    /// <returns>Eklenen kullanıcı.</returns>
    Task<User> Add(User user);
}