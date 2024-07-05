using CarRentalAPI.Domain.Entities.Common;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Kullanıcı CRUD operasyonları ve doğrulama işlemleri için depo sınıfı.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// UserRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public UserRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir kullanıcı ekler.
    /// </summary>
    /// <param name="user">Eklenecek kullanıcı bilgileri.</param>
    /// <returns>Eklenen kullanıcı.</returns>
    public async Task<User> Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    /// <summary>
    /// E-posta ve şifre ile kullanıcıyı doğrular.
    /// </summary>
    /// <param name="email">Kullanıcının e-posta adresi.</param>
    /// <param name="password">Kullanıcının şifresi.</param>
    /// <returns>Doğrulanan kullanıcı.</returns>
    public User GetUserByEmailAndPassword(string email, string password)
    {
        return _context.Users.Include(u => u.UserType).SingleOrDefault(u => u.Email == email && u.Password == password);
    }
}
