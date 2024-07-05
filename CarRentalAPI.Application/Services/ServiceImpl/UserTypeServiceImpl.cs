using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories.Repository;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Kullanıcı türü CRUD operasyonları için servis sınıfı.
/// </summary>
public class UserTypeServiceImpl : IUserTypeService
{
    private readonly IUserTypeRepository _userTypeRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// UserTypeServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="userTypeRepository">Kullanıcı türü deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public UserTypeServiceImpl(IUserTypeRepository userTypeRepository, IMapper mapper)
    {
        _userTypeRepository = userTypeRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir kullanıcı türü ekler.
    /// </summary>
    /// <param name="userTypeCreateUpdateDTO">Eklenecek kullanıcı türü bilgileri.</param>
    /// <returns>Eklenen kullanıcı türü bilgileri.</returns>
    public async Task<UserTypeCreateUpdateDTO> Add(UserTypeCreateUpdateDTO userTypeCreateUpdateDTO)
    {
        userTypeCreateUpdateDTO.isDeleted = false;
        var userType = _mapper.Map<UserType>(userTypeCreateUpdateDTO);
        var addedUserType = await _userTypeRepository.Add(userType);
        return _mapper.Map<UserTypeCreateUpdateDTO>(addedUserType);
    }


    /// <summary>
    /// Bir kullanıcı türünü siler.
    /// </summary>
    /// <param name="userTypeId">Silinecek kullanıcı türünün ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int userTypeId)
    {
        var existingUserType = await _userTypeRepository.GetByIdAsync(userTypeId);

        if (existingUserType != null)
        {
            return await _userTypeRepository.Delete(existingUserType);
        }
        return false;
    }

    /// <summary>
    /// Tüm kullanıcı türlerini getirir.
    /// </summary>
    /// <returns>Tüm kullanıcı türlerinin listesi.</returns>
    public async Task<IEnumerable<UserTypeDTO>> GetAllAsync()
    {
        var userTypes = await _userTypeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserTypeDTO>>(userTypes);
    }

    /// <summary>
    /// Belirli bir kullanıcı türünü ID ile getirir.
    /// </summary>
    /// <param name="userTypeId">Getirilecek kullanıcı türünün ID'si.</param>
    /// <returns>Belirli kullanıcı türünün bilgileri.</returns>
    public async Task<UserTypeDTO> GetByIdAsync(int userTypeId)
    {
        var userType = await _userTypeRepository.GetByIdAsync(userTypeId);
        return _mapper.Map<UserTypeDTO>(userType);
    }

    /// <summary>
    /// Bir kullanıcı türünü günceller.
    /// </summary>
    /// <param name="userTypeCreateUpdateDTO">Güncellenecek kullanıcı türü bilgileri.</param>
    /// <returns>Güncellenen kullanıcı türü bilgileri.</returns>
    public async Task<UserTypeCreateUpdateDTO> Update(UserTypeCreateUpdateDTO userTypeCreateUpdateDTO)
    {
        var userType = _mapper.Map<UserType>(userTypeCreateUpdateDTO);

        var updatedUserType = await _userTypeRepository.Update(userType);
        if (updatedUserType != null)
        {
            return _mapper.Map<UserTypeCreateUpdateDTO>(updatedUserType);
        }
        return null;
    }
}
