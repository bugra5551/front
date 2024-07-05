using AutoMapper;
using CarRentalAPI.Application.DTOs;
using CarRentalAPI.Domain.Entities.Common;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Kullanıcı CRUD operasyonları ve kimlik doğrulama için servis sınıfı.
/// </summary>
public class UserServiceImpl : IUserService
{
    private readonly IUserRepository _userRepository; 
    private readonly IMapper _mapper;

    /// <summary>
    /// UserServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="userRepository">Kullanıcı deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public UserServiceImpl(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir kullanıcı ekler.
    /// </summary>
    /// <param name="userDTO">Eklenecek kullanıcı bilgileri.</param>
    /// <returns>Eklenen kullanıcı bilgileri.</returns>
    public async Task<UserDTO> Add(UserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        var addedUser = await _userRepository.Add(user);
        return _mapper.Map<UserDTO>(addedUser);

    }

    /// <summary>
    /// Kullanıcıyı kimlik doğrulama işlemi yapar.
    /// </summary>
    /// <param name="username">Kullanıcı adı.</param>
    /// <param name="password">Şifre.</param>
    /// <returns>Doğrulanan kullanıcı.</returns>
    public User Authenticate(string username, string password)
    {
        return _userRepository.GetUserByEmailAndPassword(username, password);
    }
}
